import { Injectable } from '@angular/core';
import {Http} from '@angular/http';
import {RegisterForm} from '../viewmodels/registerform';

@Injectable()
export class AccountService {

    constructor(private http: Http){ }

    register(registerForm: RegisterForm) {
console.log(registerForm);
        const body = JSON.stringify(registerForm);
        console.log(body);
        const headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post("api/Account/Register", body, ({headers : headers}) as any);
    }
}