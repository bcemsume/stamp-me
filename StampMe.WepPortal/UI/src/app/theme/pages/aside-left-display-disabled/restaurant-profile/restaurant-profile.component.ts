import { Component, AfterViewInit, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ScriptLoaderService } from '../../../../_services/script-loader.service';
import { IMultiSelectOption, IMultiSelectTexts } from 'angular-2-dropdown-multiselect';
import { RestaurantService } from './restaurant-profile.service';
import { ImageDataDTO } from './restaurant-profile.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToasterConfig, ToasterService, Toast } from 'angular2-toaster';
import { debounce } from 'rxjs/operators/debounce';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
    moduleId: module.id,
    selector: 'restaurant-profile',
    templateUrl: 'restaurant-profile.component.html',
    styleUrls: ['restaurant-profile.component.scss'],
    providers: [RestaurantService]
})
export class RestaurantProfileComponent implements OnInit, AfterViewInit {

    imageDTO: ImageDataDTO = new ImageDataDTO();
    calismaGunleriModel: number[] = [];
    calismaGunleri: IMultiSelectOption[];

    odemeTipleriModel: number[] = [];
    odemeTipleri: IMultiSelectOption[];
    popupImgVisible: any;
    @ViewChild('btnCloseImageModal') btnCloseImageModal: ElementRef;
    @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

    form = new FormGroup({
        Info: new FormControl('', Validators.required),
        Data: new FormControl('', Validators.required),
    });

    public defaultDueDate = new Date();
    formUrunEkleme = new FormGroup({
        Id: new FormControl(null),
        Description: new FormControl('', Validators.required),
        Status: new FormControl(0, Validators.required),
        DueDate: new FormControl(this.defaultDueDate.getDate() + "/" + (this.defaultDueDate.getMonth() + 1) + "/" + this.defaultDueDate.getFullYear(), Validators.required),
    });

    formPromosyonEkleme = new FormGroup({
        Id: new FormControl(null),
        ProductId: new FormControl('', Validators.required),
        Status: new FormControl(0, Validators.required),
        Claim: new FormControl(10, Validators.required),
    });

    formMenuEkleme = new FormGroup({
        Id: new FormControl(null),
        RestId: new FormControl(null),
        Image: new FormControl(null),
        MenuDetail: new FormControl(null, Validators.required),
    });


    public config1: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right'
    });

    myTexts: IMultiSelectTexts = {
        checkAll: 'Hepsini Seç',
        uncheckAll: 'Hepsini Sil',
        checked: 'Seçili',
        checkedPlural: 'adet seçildi',
        searchPlaceholder: 'Ara',
        searchEmptyResult: 'Bulunamadı...',
        defaultTitle: 'Seç',
        allSelected: 'Hepsi Seçili',
    };
    imgDataSource: any;

    loadImages() {
        this.imgDataSource = null;
        this.svc.getRestImages().subscribe(x => {
            this.imgDataSource = x;
        });
    }

    loadUrunler() {
        this.urunListesi = null;
        this.svc.getProducts().subscribe(x => {
            this.urunListesi = x
        });
    }

    loadPromosyonlar() {
        this.promosyonListesi = null;
        this.svc.getPromotions().subscribe(x => {
            this.promosyonListesi = x
        });
    }

    loadMenu() {
        this.menuListesi = null;
        this.svc.getMenus().subscribe(x => {
            this.menuListesi = x
        });
    }

    ngOnInit() {
        this.loadImages();
        this.calismaGunleri = [
            { id: 1, name: 'Pazartesi' },
            { id: 2, name: 'Salı' },
            { id: 3, name: 'Çarşamba' },
            { id: 4, name: 'Perşembe' },
            { id: 5, name: 'Cuma' },
            { id: 6, name: 'Cumartesi' },
            { id: 7, name: 'Pazar' },
        ];

        this.odemeTipleri = [
            { id: 1, name: 'Sodexho' },
            { id: 2, name: 'Sodexho Yemek Çeki' },
            { id: 3, name: 'Multinet' },
            { id: 4, name: 'SetCard' },
            { id: 5, name: 'Kredi Kartı' },
        ];

        this.svc.getRestaurantInfo().subscribe(x => {
            this.phone = x.phone;

            x.paymentTypes.split(',').forEach(element => {
                this.odemeTipleriModel.push(+element);
            });

            x.workingDays.split(',').forEach(element => {
                this.calismaGunleriModel.push(+element);
            });

            for (let index = 0; index < x.workingHours.split(',').length; index++) {
                this.someRange[index] = +x.workingHours.split(',')[index];

            }
            x.workingHours.split(',').forEach(element => {
            });

        });

        this.svc.getMenus().subscribe(x => {
            debugger;
            console.log(x);
            this.menuListesi = x;
        });

        this.svc.getAll().subscribe(x => {
            this.urunListesi = x["product"];
            this.promosyonListesi = x["promotion"];

        });
    }
    toasterService: ToasterService;

    constructor(private _script: ScriptLoaderService, private svc: RestaurantService, toasterService: ToasterService) {
        this.toasterService = toasterService;
    }


    someKeyboardConfig: any = {
        behaviour: 'drag',
        connect: true,
        keyboard: true,  // same as [keyboard]="true"
        step: 0.5,
        pageSteps: 10,  // number of page steps, defaults to 10
        range: {
            min: 0,
            max: 23
        },
        pips: {
            mode: 'count',
            density: 2,
            values: 6,
            stepped: true
        }
    };

    someRange = [9, 21];

    onFileChange(event) {
        let reader = new FileReader();
        if (event.target.files && event.target.files.length > 0) {
            let file = event.target.files[0];
            reader.readAsDataURL(file);
            reader.onload = () => {
                this.form.get('Data').setValue(reader.result.split(',')[1]);
            };
        }
    }

    onFileChangeMenu(event) {
        debugger;
        let reader = new FileReader();
        if (event.target.files && event.target.files.length > 0) {
            let file = event.target.files[0];
            reader.readAsDataURL(file);
            reader.onload = () => {
                this.formMenuEkleme.get('Image').setValue(reader.result.split(',')[1]);
            };
        }
    }

    phone: any;
    urunListesi: any;
    promosyonListesi: any;
    menuListesi: any;

    btnCalismaOdeme() {
        debugger;
        let data = {
            Phone: this.phone,
            WorkingDays: this.calismaGunleriModel.join(','),
            WorkingHours: this.someRange.join(','),
            PaymentTypes: this.odemeTipleriModel.join(',')
        };

        this.svc.SaveRestaurantInfo(data).subscribe(x => {

            this.popSubmitToast();

        });
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

    onSubmit() {
        this.svc.addImage(this.form.value).subscribe(x => {
            document.getElementById('btnCloseImageModal').click();
            this.loadImages();
        });
    }

    onSubmitUrunEkle() {
        debugger;
        this.svc.addUrun(this.formUrunEkleme.value).subscribe(x => {
            document.getElementById('btnCloseUrunEkleModal').click();
            this.loadUrunler();
        });
    }

    onSubmitPromosyonEkle() {
        debugger;
        this.svc.addPromosyon(this.formUrunEkleme.value).subscribe(x => {
            document.getElementById('btnCloseUrunEkleModal').click();
            this.loadPromosyonlar();
        });
    }

    onSubmitMenuEkle() {
        debugger;
        this.svc.addMenu(this.formMenuEkleme.value).subscribe(x => {
            document.getElementById('btnCloseUrunEkleModal').click();
            this.loadMenu();
        });
    }

    btnEditUrun(data) {
        if (data.instance.getSelectedRowsData().length <= 0) {
            document.getElementById('btnCloseUrunEkleModal').click();
            this.popErrorToast('Lütfen bir kayıt seçiniz');
            return;
        }
        var item = data.instance.getSelectedRowsData()[0];
        debugger;
        this.formUrunEkleme.setValue({
            Id: item.id,
            Description: item.description,
            Status: item.status,
            DueDate: item.dueDate
        });
    }

    btnEditPromosyon(data) {
        if (data.instance.getSelectedRowsData().length <= 0) {
            document.getElementById('btnCloseUrunEkleModal').click();
            this.popErrorToast('Lütfen bir kayıt seçiniz');
            return;
        }
        var item = data.instance.getSelectedRowsData()[0];
        debugger;
        this.formPromosyonEkleme.setValue({
            Id: item.id,
            ProductId: item.productId,
            Status: item.status,
            Claim: item.claim
        });
    }

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
        this._script.load('restaurant-profile',
            'assets/demo/default/custom/components/forms/widgets/bootstrap-select.js');
        this._script.load('restaurant-profile',
            'assets/demo/default/custom/components/forms/widgets/input-mask.js');
    }

}
