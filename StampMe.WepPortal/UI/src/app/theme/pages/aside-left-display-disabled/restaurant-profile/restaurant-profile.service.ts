import { Injectable } from '@angular/core';
import { HttpService } from '../../../../shared/http.service';
import { ImageDataDTO, UrunEkleDTO, PromosyonEkleDTO, MenuEkleDTO } from './restaurant-profile.model';

@Injectable()
export class RestaurantService {


    constructor(private http: HttpService) {


    }

    // token will added automatically to get request header
    addImage(imageData: ImageDataDTO) {
        return this.http.post(`restaurant/AddImageAsync?id=` + this.http.currentUser.id, imageData).map((res) => {
            return res;
        });
    }

    addUrun(urunEkleData: UrunEkleDTO) {
        debugger;
        return this.http.post(`restaurant/AddUpdateProduct?id=` + this.http.currentUser.id, urunEkleData).map((res) => {
            return res;
        });
    }

    addPromosyon(PromosyonEkleData: PromosyonEkleDTO) {
        debugger;
        return this.http.post(`restaurant/AddUpdatePromotion?id=` + this.http.currentUser.id, PromosyonEkleData).map((res) => {
            return res;
        });
    }

    addMenu(MenuEkleData) {
        debugger;
        MenuEkleData.restId = this.http.currentUser.id;
        return this.http.post('restaurant/MenuSave', MenuEkleData).map((res) => {
            return res;
        });
    }

    getAll() {
        return this.http.get(`restaurant/Get/` + this.http.currentUser.id).map(x => x.json());
    }

    getProducts() {
        return this.http.get(`restaurant/Get/` + this.http.currentUser.id).map(x => x.json()["product"]);
    }

    getPromotions() {
        return this.http.get(`restaurant/Get/` + this.http.currentUser.id).map(x => x.json()["promotions"]);
    }

    getMenus() {
        debugger;
        return this.http.get(`restaurant/GetMenuList?id=` + this.http.currentUser.id).map(x => x.json());
    }

    getRestImages() {
        return this.http.get(`restaurant/GetImages?id=` + this.http.currentUser.id).map(x => x.json());
    }

    DeleteImageAsync(imgId) {
        return this.http.get(`restaurant/GetImages?id=` + this.http.currentUser.id + '&imgId=' + imgId).map(x => x.json());

    }

    deleteRestaurant(item) {
        return this.http.get(`restaurant/DeleteAsync?id=` + item).map(x => x);
    }

    // SaveRestaurantInfo
    SaveRestaurantInfo(data) {
        debugger;
        data.Id = this.http.currentUser.id;
        return this.http.post(`restaurant/SaveRestaurantInfo`, data).map(x => x);
    }

    getRestaurantInfo() {
        return this.http.get(`restaurant/GetRestaurantInfo?Id=` + this.http.currentUser.id).map(x => x.json());
    }

}