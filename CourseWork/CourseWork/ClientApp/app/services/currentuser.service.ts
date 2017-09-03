import { Injectable } from '@angular/core';
import {Http} from '@angular/http';

@Injectable()
export class CurrentUserService {

    constructor(private http: Http){ }

    getCurrentUserInfo() {
        return this.http.get("api/CurrentUser/GetCurrentUserInfo");
    }
}