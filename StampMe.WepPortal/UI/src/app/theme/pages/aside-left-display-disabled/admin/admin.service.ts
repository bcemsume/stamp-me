import { Injectable } from '@angular/core';
import { HttpService } from '../../../../shared/http.service';

@Injectable()
export class AdminService {
    constructor(private http: HttpService) { }

    // token will added automatically to get request header
    getRestaurantList() {
        return this.http.get(`restaurant/GetAdminRestaurantList`).map((res) => {
            return res.json();
        });
    }

    saveRestaurant(item) {
        return this.http.post(`restaurant/QuickSave`, item).map(x => x);
    }

    saveContract(item) {
        debugger;
        return this.http.post(`contract/Add`, item).map(x => x);
    }

    updateContract(item) {
        debugger;
        return this.http.post(`contract/UpdateAsync`, item).map(x => x);
    }

    deleteRestaurant(item) {
        return this.http.get(`restaurant/DeleteAsync?id=` + item).map(x => x);
    }

    deleteContract(item) {
        return this.http.post(`contract/DeleteAsync`, item).map(x => x);
    }

    getContract() {
        return this.http.get(`contract/GetAllAsync`).map(x => x.json());
    }

    getImages() {
        return this.http.get(`restaurant/GetWaitingApprovalImage`).map(x => x.json());
    }

    GetWaitingApprovalProduct() {
        return this.http.get('restaurant/GetWaitingApprovalProduct').map(x => x.json());
    }

    GetWaitingApprovalImage() {
        return this.http.get('restaurant/GetWaitingApprovalImage').map(x => x.json());
    }

    GetWaitingApprovalPromotion() {
        return this.http.get('restaurant/GetWaitingApprovalPromotion').map(x => x.json());
    }

    ApprovedProduct(item) {
        return this.http.post('restaurant/ApprovedProduct', item).map(x => x);
    }

    ApprovedImage(item) {
        return this.http.post('restaurant/ApprovedImageAsync', item).map(x => x);
    }

    ApprovedPromotion(item) {
        return this.http.post('restaurant/ApprovedPromotion', item).map(x => x);
    }

    RejectProduct(item) {
        return this.http.post('restaurant/RejectProduct', item).map(x => x);
    }

    RejectPromotion(item) {
        return this.http.post('restaurant/RejectPromotion', item).map(x => x);
    }

    RejectImage(item) {
        return this.http.post('restaurant/RejectImage', item).map(x => x);
    }
}