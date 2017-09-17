import { Injectable } from '@angular/core';
import {Http, Headers, RequestOptions} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import { Response} from '@angular/http';

@Injectable()
export class BaseService {
    constructor(protected http: Http) {}

    protected  requestPost(path: string, argument: any) {
        const body = JSON.stringify(argument);
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });
        return this.http.post(path, body, options).map(this.getData).catch(this.throwError);
    }

    protected requestGet(path: string) {
        return this.http.get(path).map(this.getData).catch(this.throwError);
    }

    protected requestGetWithParams(path: string, params: any) {
        const requestOptions = new RequestOptions();
        requestOptions.search = params;
        return this.http.get(path, requestOptions).map(this.getData).catch(this.throwError);
    }

    private getData(response: Response) {
        if (response.text() !== "") {
            return response.json();
        }
        return null;
    }

    private throwError(error: any) {
        return Observable.throw(error);
    }
}