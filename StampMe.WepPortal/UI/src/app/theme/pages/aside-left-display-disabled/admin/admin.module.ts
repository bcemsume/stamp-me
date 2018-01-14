import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { LayoutModule } from '../../../layouts/layout.module';
import { AsideLeftDisplayDisabledComponent } from '../aside-left-display-disabled.component';
import { HttpService } from '../../../../shared/http.service';
import { XHRBackend, RequestOptions } from '@angular/http';
import {
    DxDataGridModule,
    DxSparklineModule,
    DxTemplateModule,
    DxPopupModule,
    DxSelectBoxModule
} from 'devextreme-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToasterModule } from 'angular2-toaster';
import { NgDatepickerModule } from 'ng2-datepicker';
const routes: Routes = [
    {
        "path": "",
        "component": AsideLeftDisplayDisabledComponent,
        "children": [
            {
                "path": "",
                "component": AdminComponent
            }
        ]
    }
];
@NgModule({
    imports: [
        CommonModule, RouterModule.forChild(routes), LayoutModule, DxDataGridModule, FormsModule, ReactiveFormsModule, ToasterModule, NgDatepickerModule, DxPopupModule,
        DxSelectBoxModule
    ], exports: [
        RouterModule
    ],
    declarations: [
        AdminComponent
    ]
})
export class AdminModule {



}