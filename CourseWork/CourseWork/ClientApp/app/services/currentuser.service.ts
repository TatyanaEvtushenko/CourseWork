import { Injectable } from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import { Response} from '@angular/http';

@Injectable()
export class CurrentUserService {

    constructor(private http: Http){ }

    getCurrentUserInfo() {
        return this.http.get("api/CurrentUser/GetCurrentUserInfo")
                        .map((response: Response) => response.json())
                        .catch((error: any) => Observable.throw(error));
    }
}