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
        isActive: new FormControl('', Validators.required),
        Id: new FormControl(''),
    });

    popupTitle: any;


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
        });
    }

    onSubmit() {
        if (!this.form.invalid) {
            console.log(this.form.value);

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