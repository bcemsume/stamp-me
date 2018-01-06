import { Component, OnInit, ViewEncapsulation, AfterViewInit, ElementRef, ViewChild, Renderer } from '@angular/core';
import { Helpers } from '../../../../helpers';
import { ScriptLoaderService } from '../../../../_services/script-loader.service';
import { AdminService } from './admin.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { formArrayNameProvider } from '@angular/forms/src/directives/reactive_directives/form_group_name';
import * as $ from "jquery";
import { ToasterConfig, ToasterService, Toast } from 'angular2-toaster';
import { DxDataGridComponent } from 'devextreme-angular';
import { errorHandler } from '@angular/platform-browser/src/browser';
import { DatepickerOptions } from 'ng2-datepicker';
import * as trLocale from 'date-fns/locale/tr';
import { ProductDTO } from '../../../../shared/product.mode';
import { PromotionDTO } from '../../../../shared/promotion.model';

@Component({
    selector: ".m-grid__item.m-grid__item--fluid.m-wrapper",
    templateUrl: "./admin.component.html",
    encapsulation: ViewEncapsulation.None,
    providers: [AdminService]
})
export class AdminComponent implements OnInit, AfterViewInit {

    @ViewChild('btnCloseModal') btnCloseModal: ElementRef;
    @ViewChild('btnCloseConfirmModal') btnCloseConfirmModal: ElementRef;


    @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
    public config1: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right'
    });
    toasterService: ToasterService;
    form = new FormGroup({
        Name: new FormControl('', Validators.required),
        Email: new FormControl('', Validators.compose([Validators.email, Validators.required])),
        UserName: new FormControl('', Validators.required),
        Password: new FormControl('', Validators.required),
        Adress: new FormControl('', Validators.required),
        isActive: new FormControl(true, Validators.required),
        isPromo: new FormControl(false, Validators.required),
        Id: new FormControl(''),
        Product: new FormControl(''),
        Promotion: new FormControl(''),
    });
    status = [{ Name: "Onaylı", Value: "Approved" }, { Name: "Beklemede", Value: "WaitApproval" }]


    formProduct = new FormGroup({
        Description: new FormControl('', Validators.required),
        Status: new FormControl(this.status[0].Value, Validators.required),
        DueDate: new FormControl(new Date("2019-01-01"), Validators.required),
    });

    formPromotion = new FormGroup({
        Product: new FormControl('', Validators.required),
        Status: new FormControl(this.status[0].Value, Validators.required),
        Claim: new FormControl('', Validators.required),
    });


    options: DatepickerOptions = {
        minYear: 1970,
        maxYear: 2080,
        displayFormat: 'MM.DD.YYYY',
        barTitleFormat: 'MMMM YYYY',
        firstCalendarDay: 1, // 0 - Sunday, 1 - Monday
        locale: trLocale,
    };

    prodcutsDTO: ProductDTO[] = [{ Description: "", Status: "", DueDate: "", Id: 0 }]
    promotionDTO: PromotionDTO[] = [{ Claim: "", Status: "", ProductId: 0 }]

    popupTitle: any;

    products: any = [];

    constructor(private _script: ScriptLoaderService, private svc: AdminService, private renderer: Renderer, toasterService: ToasterService) {
        this.toasterService = toasterService;
    }

    dataSource: any;

    ngOnInit() {
        this.gridLoad();
    }

    gridLoad() {
        this.svc.getRestaurantList().subscribe(x => this.dataSource = x);
    }

    ngAfterViewInit() {

        Helpers.bodyClass('m-page--wide m-header--fixed m-header--fixed-mobile m-footer--push m-aside--offcanvas-default');
        this._script.load('.m-grid__item.m-grid__item--fluid.m-wrapper',
            'assets/demo/default/custom/components/forms/widgets/bootstrap-datepicker.js');
        this._script.load('.m-grid__item.m-grid__item--fluid.m-wrapper',
            'assets/demo/default/custom/components/forms/widgets/bootstrap-select.js');

    }

    btnAddProduct() {
        if (!this.formProduct.invalid) {
            let product = new ProductDTO();
            product.Description = this.formProduct.value.Description;
            product.Status = this.formProduct.value.Status;
            product.DueDate = this.formProduct.value.DueDate.toLocaleDateString('tr-TR');
            product.Id = this.prodcutsDTO[this.prodcutsDTO.length - 1].Id + 1;

            this.prodcutsDTO.push(product);
            this.prodcutsDTO = this.prodcutsDTO.filter(x => x.Description != "");

            this.formProduct.patchValue({ Description: '', Status: 'Approved', DueDate: new Date("2019-01-01") });
            console.log(this.products);

            document.getElementById('btnCloseConfirmProductModal').click();
        }

    }


    btnAddPromotion() {
debugger;
        if (!this.formPromotion.invalid) {
            let promotion = new PromotionDTO();
            promotion.Claim = this.formPromotion.value.Claim;
            promotion.Status = this.formPromotion.value.Status;
            promotion.ProductId = this.formPromotion.value.Product

            this.promotionDTO.push(promotion);
            this.promotionDTO = this.promotionDTO.filter(x => x.Claim != "");

            this.formPromotion.patchValue({ Claim: "", Status: "Approved", ProductId: "" });
            console.log(this.products);

            document.getElementById('btnCloseConfirmPromotionModal').click();
        }
    }


    prductCellCustom(data) {
        debugger;
        let row = this.prodcutsDTO.find(x => x.Id == data);
        if (row) {
            return this.prodcutsDTO.find(x => x.Id == data).Description;
        }
    }

    btnDelete() {
        console.log(this.dataGrid.instance.getSelectedRowsData());

        this.dataGrid.instance.getSelectedRowsData().forEach(x => {


            this.svc.deleteRestaurant(x.id).subscribe(z => {
                console.log(this.dataGrid.instance.getSelectedRowsData()[this.dataGrid.instance.getSelectedRowsData().length - 1].Id);
                console.log(x.Id);

                if (this.dataGrid.instance.getSelectedRowsData()[this.dataGrid.instance.getSelectedRowsData().length - 1].Id == x.Id) {
                    this.popSubmitToast();
                    document.getElementById('btnCloseConfirmModal').click();
                    this.gridLoad();
                }

            }, err => {
                console.log(err);
                this.popErrorToast(err)
            });
        }
        );


    }

    popSubmitToast() {
        var toast: Toast = {
            type: 'success',
            title: 'Başarılı',
            body: 'İşleminiz başaarılı bir şekilde gerçekleştirilmiştir.'
        };

        this.toasterService.pop(toast);
    }

    popErrorToast(body) {
        var toast: Toast = {
            type: 'error',
            title: 'Hata',
            body: body
        };

        this.toasterService.pop(toast);
    }

    btnEdit() {
        if (this.dataGrid.instance.getSelectedRowsData().length <= 0) {
            document.getElementById('btnCloseModal').click();
            this.popErrorToast('Lütfen bir kayıt seçiniz');
            return;
        }
        var item = this.dataGrid.instance.getSelectedRowsData()[0];

        this.form.setValue({
            Name: item.name,
            Email: item.email,
            Password: item.password,
            UserName: item.userName,
            Adress: item.adress,
            isActive: item.isActive,
            Id: item.id,
            isPromo : item.isPromo,
            Product: null,
            Promotion: null
        });
    }

    sendValue:any;

    onSubmit() {
        if (!this.form.invalid) {
            this.sendValue = this.form.value;
            this.sendValue.Product = this.prodcutsDTO;
            this.sendValue.Promotion = this.promotionDTO;

            this.svc.saveRestaurant(this.form.value).subscribe(x => {
                document.getElementById('btnCloseModal').click();
                this.popSubmitToast();
                this.gridLoad();
                this.form.reset();
            }, err => {
                console.log(err);
                this.popErrorToast(err)
            }
            );
        }

    }

}