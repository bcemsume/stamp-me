import { Component, OnInit, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { Helpers } from '../../../helpers';

declare let mLayout: any;
@Component({
    selector: "app-header-nav",
    templateUrl: "./header-nav.component.html",
    encapsulation: ViewEncapsulation.None,
})
export class HeaderNavComponent implements OnInit, AfterViewInit {

    currentUser: any;
    constructor() {

    }
    
    ngOnInit() {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));

    }
    ngAfterViewInit() {

        mLayout.initHeader();

    }

}