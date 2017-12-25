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

    deleteRestaurant(item) {
        return this.http.get(`restaurant/DeleteAsync?id=` + item).map(x => x);
    }
}