﻿import { Injectable, EventEmitter } from '@angular/core';
import { BaseService} from './base.service';
import {RegisterForm} from '../viewmodels/registerform';
import {LoginForm} from '../viewmodels/loginform';

@Injectable()
export class AccountService extends BaseService{
    isAuthChanged = new EventEmitter<boolean>();

    changeAuthState(isLoggedIn: boolean) {
        this.isAuthChanged.emit(isLoggedIn);
    }

    register(registerForm: RegisterForm) {
        return this.requestPost("api/Account/Register", registerForm);
    }

    login(loginForm: LoginForm) {
        return this.requestPost("api/Account/Login", loginForm);
    }

    logout() {
        return this.requestGet("api/Account/LogOut");
    }
}