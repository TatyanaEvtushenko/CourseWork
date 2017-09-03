import { Injectable } from '@angular/core';
import { BaseService} from './base.service';
import {RegisterForm} from '../viewmodels/registerform';
import {LoginForm} from '../viewmodels/loginform';

@Injectable()
export class AccountService extends BaseService{

    register(registerForm: RegisterForm) {
        return this.requestPost("api/Account/Register", registerForm);
    }

    login(loginForm: LoginForm) {
        return this.requestPost("api/Account/Login", loginForm);
    }
}