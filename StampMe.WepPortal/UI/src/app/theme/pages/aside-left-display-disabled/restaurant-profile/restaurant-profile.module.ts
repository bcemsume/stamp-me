// Angular Imports
import { NgModule } from '@angular/core';

// This Module's Components
import { RestaurantProfileComponent } from './restaurant-profile.component';
import { Routes, RouterModule } from '@angular/router';
import { AsideLeftDisplayDisabledComponent } from '../aside-left-display-disabled.component';
import { CommonModule } from '@angular/common';
import { LayoutModule } from '../../../layouts/layout.module';
import { DxDataGridModule, DxPopupModule } from 'devextreme-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToasterModule } from 'angular2-toaster';
import { NouisliderModule } from 'ng2-nouislider';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { AgmCoreModule } from '@agm/core';

const routes: Routes = [
    {
        "path": "",
        "component": AsideLeftDisplayDisabledComponent,
        "children": [
            {
                "path": "",
                "component": RestaurantProfileComponent
            }
        ]
    }
];

@NgModule({
    imports: [
        CommonModule, DxPopupModule, RouterModule.forChild(routes), MultiselectDropdownModule, LayoutModule, DxDataGridModule, FormsModule, ReactiveFormsModule, ToasterModule, NouisliderModule, AgmCoreModule.forRoot({
            apiKey: 'AIzaSyDy5VP8zq7zt5iDKPmqBy5lvnM5tATjEjc'
          })
    ], exports: [
        RouterModule
    ],
    declarations: [
        RestaurantProfileComponent
    ]
})
export class RestaurantProfileModule {

//AIzaSyDy5VP8zq7zt5iDKPmqBy5lvnM5tATjEjc
}
