import { Injectable } from '@angular/core';
import { Http, XHRBackend, RequestOptions, Request, RequestOptionsArgs, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { APP_INITIALIZER } from '@angular/core/src/application_init';

@Injectable()
export class HttpService extends Http {

    currentUser: any;
    constructor(backend: XHRBackend, options: RequestOptions) {
        let token = localStorage.getItem('auth_token');
        options.headers.set('Authorization', `Bearer ${token}`);
        super(backend, options);
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        console.log(url);
        let apiURL = "http://185.187.186.41:90/api/";
        let token = localStorage.getItem('auth_token');
        if (typeof url === 'string') {
            if (!options) {
                options = { headers: new Headers() };
            }
            options.headers.set('Authorization', `Bearer ${token}`);
        } else {
            url.headers.set('Authorization', `Bearer ${token}`);
            url.url = apiURL + url.url;
        }
        return super.request(url).catch(this.catchAuthError(this));
    }

    private catchAuthError(self: HttpService) {
        return (res: Response) => {
            console.log(res);
            if (res.status === 401 || res.status === 403) {
                console.log(res);
            }
            return Observable.throw(res);
        };
    }
}