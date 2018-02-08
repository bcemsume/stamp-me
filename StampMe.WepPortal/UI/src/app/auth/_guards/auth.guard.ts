import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { UserService } from "../_services/user.service";
import { Observable } from "rxjs/Rx";
import { retry } from "rxjs/operator/retry";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private _router: Router, private _userService: UserService) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        debugger;
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));

        if (currentUser) {
            if (!currentUser.isAdmin && state.url.indexOf('admin') > -1) {

                return false;
            }else {
                return true;
            }

        }

        this._router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;


    }
}