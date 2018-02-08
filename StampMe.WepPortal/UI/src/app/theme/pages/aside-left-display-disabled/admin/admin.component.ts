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
    styles: [`.modal-lg {
        max-width: 95vw;
    }; 
    `],
    providers: [AdminService]
})
export class AdminComponent implements OnInit, AfterViewInit {

    @ViewChild('btnCloseModal') btnCloseModal: ElementRef;
    @ViewChild('btnCloseConfirmModal') btnCloseConfirmModal: ElementRef;
    @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;


    waitingProduct: any;
    waitingPromotion: any;
    waitingImage: any;

    public config1: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right'
    });
    toasterService: ToasterService;
    form = new FormGroup({
        Name: new FormControl('', Validators.required),
        Email: new FormControl('', Validators.compose([Validators.email, Validators.required])),
        UserName: new FormControl('', Validators.required),
        Password: new FormControl('', Validators.required),
        ContractId: new FormControl('', Validators.required),
        Adress: new FormControl('', Validators.required),
        isActive: new FormControl(''),
        isPromo: new FormControl(''),
        Id: new FormControl(''),
        Product: new FormControl(''),
        Promotion: new FormControl(''),
        Latitude: new FormControl(''),
        Longitude:new FormControl('')
    });
    status = [{ Name: "Onaylı", Value: "Approved" }, { Name: "Beklemede", Value: "WaitApproval" }]

    formContract = new FormGroup({
        Type: new FormControl('', Validators.required),
        Price: new FormControl('', Validators.required),
        Description: new FormControl('', Validators.required),
        Id: new FormControl(''),
    });

    btnPromosyonOnay(data) {
        this.svc.ApprovedPromotion({ restId: data.key.restId, PromId: data.key.promotionId }).subscribe(x => { this.popSubmitToast(); this.waitingApPromotion() });
    }

    btnPromosyonRed(data) {
        this.svc.RejectPromotion({ restId: data.key.restId, promotionId: data.key.promotionId }).subscribe(x => { this.popSubmitToast(); this.waitingApPromotion() });
    }

    btnUrunOnay(data) {
        this.svc.ApprovedProduct({ restId: data.key.restId, ProdId: data.key.productId }).subscribe(x => { this.popSubmitToast(); this.waitingApProduct() });
    }

    btnUrunRed(data) {
        this.svc.RejectProduct({ restId: data.key.restId, productId: data.key.productId }).subscribe(x => { this.popSubmitToast(); this.waitingApProduct() });
    }

    btnImagesOnay(data) {
        this.svc.ApprovedImage({ restId: data.key.restId, imageId: data.key.id }).subscribe(x => { this.popSubmitToast(); this.waitingApImage() });
    }

    btnImagesRed(data) {
        this.svc.RejectImage({ restId: data.key.restId, imageId: data.key.id }).subscribe(x => { this.popSubmitToast(); this.waitingApImage() });
    }

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

    contractSource: any;
    imagesSource: any;

    constructor(private _script: ScriptLoaderService, private svc: AdminService, private renderer: Renderer, toasterService: ToasterService) {
        this.toasterService = toasterService;
    }

    dataSource: any;

    waitingApProduct() {
        this.svc.GetWaitingApprovalProduct().subscribe(x => this.waitingProduct = x);
    }

    waitingApImage() {
        this.svc.GetWaitingApprovalImage().subscribe(x => this.imagesSource = x);
    }
    waitingApPromotion() {
        this.svc.GetWaitingApprovalPromotion().subscribe(x => this.waitingPromotion = x);
    }

    ngOnInit() {
        this.gridLoad();
        this.waitingApProduct();
        this.waitingApPromotion();
        this.contractLoad();
        this.imagesLoad();
    }

    gridLoad() {
        this.svc.getRestaurantList().subscribe(x => this.dataSource = x);
    }
    popupImgVisible: any;
    lastRowCLickedId: number;
    lastRowClickedTime: Date;
    imagePath: any;
    rowClick(e) {
        var currentTime = new Date();
        if (e.rowIndex === this.lastRowCLickedId && ((currentTime.getTime() - this.lastRowClickedTime.getTime()) < 300)) {
            this.popupImgVisible = true;
            this.imagePath = e.data.data;
        } else {
            this.lastRowCLickedId = e.rowIndex;
            this.lastRowClickedTime = new Date();
        }
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

            document.getElementById('btnCloseConfirmPromotionModal').click();
        }
    }


    prductCellCustom(data) {
        let row = this.prodcutsDTO.find(x => x.Id == data);
        if (row) {
            return this.prodcutsDTO.find(x => x.Id == data).Description;
        }
    }

    btnDelete() {

        this.dataGrid.instance.getSelectedRowsData().forEach(x => {


            this.svc.deleteRestaurant(x.id).subscribe(z => {

                if (this.dataGrid.instance.getSelectedRowsData()[this.dataGrid.instance.getSelectedRowsData().length - 1].Id == x.Id) {
                    this.popSubmitToast();
                    document.getElementById('btnCloseConfirmModal').click();
                    this.gridLoad();
                }

            }, err => {
                this.popErrorToast(err)
            });
        }
        );
    }

    btnDeleteContract(data) {
        debugger;
        data.instance.getSelectedRowsData().forEach(x => {


            this.svc.deleteContract(x).subscribe(z => {

                if (data.instance.getSelectedRowsData()[data.instance.getSelectedRowsData().length - 1].Id == x.Id) {
                    this.popSubmitToast();
                    document.getElementById('btnCloseConfirmModalContract').click();
                    this.contractLoad();
                }

            }, err => {
                this.popErrorToast(err)
            });
        }
        );
    }

    popSubmitToast() {
        var toast: Toast = {
            type: 'success',
            title: 'Başarılı',
            body: 'İşleminiz başarılı bir şekilde gerçekleştirilmiştir.'
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
            isPromo: item.isPromo,
            Product: null,
            Promotion: null,
            ContractId: this.contractSource.find( x=> x.type == item.contractName).id,
            Latitude: item.latitude,
            Longitude: item.longitude
        });
    }

    btnEditContract(data) {
        debugger;
        if (data.instance.getSelectedRowsData().length <= 0) {
            setTimeout(() => {
                document.getElementById('btnCloseContractModal').click();
                this.popErrorToast('Lütfen bir kayıt seçiniz');
                event.preventDefault();
                return false;
            }, 1000);
        } else {
            var item = data.instance.getSelectedRowsData()[0];

            this.formContract.setValue({
                Type: item.type,
                Price: item.price,
                Description: item.description,
                Id: item.id
            });
        }
    }

    sendValue: any;

    onSubmit() {
        if (!this.form.invalid) {
            debugger;
            this.sendValue = this.form.value;
            this.sendValue.Product = this.prodcutsDTO;
            this.sendValue.Promotion = this.promotionDTO;
            this.svc.saveRestaurant(this.sendValue).subscribe(x => {
                document.getElementById('btnCloseModal').click();
                this.popSubmitToast();
                this.gridLoad();
                this.form.reset();
                this.sendValue = null;
                this.promotionDTO = null;
                this.prodcutsDTO = null;
            }, err => {
                this.popErrorToast(err)
            }
            );
        }

    }

    removeUrun(data) {
        debugger;

        data.instance.getSelectedRowsData().forEach(x => {
            this.prodcutsDTO = this.prodcutsDTO.filter(z => z.Id != x.Id)
        }
        );
        if (this.prodcutsDTO.length == 0) {
            this.prodcutsDTO = [{ Description: "", Status: "", DueDate: "", Id: 0 }]
        }
    }


    removePromosyon(data) {

        data.instance.getSelectedRowsData().forEach(x => {

            this.promotionDTO = this.promotionDTO.filter(z => z.ProductId != x.ProductId)
        }
        );

        if (this.promotionDTO.length == 0) {
            this.promotionDTO = [{ Claim: "", Status: "", ProductId: 0 }]
        }
    }

    contractLoad() {
        this.svc.getContract().subscribe(x => {
            this.contractSource = x;
        });
    }

    imagesLoad() {
        this.svc.getImages().subscribe(x => {
            this.imagesSource = x;
        });
    }

    onSubmitContract() {
        if (!this.formContract.invalid) {
            debugger;
            if (this.formContract.value.Id === null) {

                this.svc.saveContract(this.formContract.value).subscribe(x => {
                    document.getElementById('btnCloseContractModal').click();
                    this.popSubmitToast();
                    this.contractLoad();
                    this.formContract.reset();
                }, err => {
                    this.popErrorToast(err)
                }
                );
            } else {

                this.svc.updateContract(this.formContract.value).subscribe(x => {
                    document.getElementById('btnCloseContractModal').click();
                    this.popSubmitToast();
                    this.contractLoad();
                    this.formContract.reset();
                }, err => {
                    this.popErrorToast(err)
                }
                );
            }
        }
    }

    onDeleteContract(item) {
        if (!this.formContract.invalid) {

            this.svc.saveContract(this.formContract.value).subscribe(x => {
                document.getElementById('btnCloseContractModal').click();
                this.popSubmitToast();
                this.contractLoad();
                this.formContract.reset();
            }, err => {
                this.popErrorToast(err)
            }
            );
        }
    }

}