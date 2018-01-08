import { Component, AfterViewInit, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ScriptLoaderService } from '../../../../_services/script-loader.service';
import { IMultiSelectOption, IMultiSelectTexts } from 'angular-2-dropdown-multiselect';
import { RestaurantService } from './restaurant-profile.service';
import { ImageDataDTO } from './restaurant-profile.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToasterConfig, ToasterService, Toast } from 'angular2-toaster';

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

    form = new FormGroup({
        Info: new FormControl('', Validators.required),
        Data: new FormControl('', Validators.required),
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
            this.imgDataSource = x; console.log(x);
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
    }
    toasterService: ToasterService;

    constructor(private _script: ScriptLoaderService, private svc: RestaurantService, toasterService: ToasterService) {
        this.toasterService = toasterService;
    }


    someKeyboardConfig: any = {
        behaviour: 'drag',
        connect: true,
        start: [9, 21],
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

    someRange: any;

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
