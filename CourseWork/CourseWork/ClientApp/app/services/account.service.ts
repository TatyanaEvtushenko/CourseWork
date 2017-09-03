import { Injectable } from '@angular/core';
import {Http} from '@angular/http';
import {RegisterForm} from '../viewmodels/registerform';
import {Observable} from 'rxjs/Observable';
import { Response} from '@angular/http';

@Injectable()
export class AccountService {

    constructor(private http: Http){ }

    register(registerForm: RegisterForm) {
        const body = JSON.stringify(registerForm);
        const headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post("api/Account/Register", body, headers)
                        .map((response: Response) => response.json())
                        .catch((error: any) => Observable.throw(error));
    }
}