import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import "rxjs/add/operator/map";
import { debounce } from "rxjs/operator/debounce";
import { HttpService } from "../../shared/http.service";

@Injectable()
export class AuthenticationService {

    constructor(private http: HttpService) { }

    login(email: string, password: string) {
        debugger;
        return this.http.get('restaurant/Login?userName=' + email + '&password=' + password)
            .map(x => x.json());
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}