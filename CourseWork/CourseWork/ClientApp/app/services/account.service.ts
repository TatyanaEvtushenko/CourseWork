import { Injectable, EventEmitter } from '@angular/core';
import { BaseService} from './base.service';
import {RegisterForm} from '../viewmodels/registerform';
import { LoginForm } from '../viewmodels/loginform';
import { ConfirmationForm } from '../viewmodels/confirmationForm';
import { UserInfo } from "../viewmodels/userinfo";

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

    confirmAccount(confirmationForm: ConfirmationForm) {
        return this.requestPost("api/UnconfirmedUser/ConfirmAccount", confirmationForm);
    }

    getUserList() {
        return this.requestGet("api/Admin/GetAllUsers");
    }

    getFilteredUserList(filter: any) {
        return this.requestGetWithParams("api/Admin/GetFilteredUsers", filter);
    }

    getPersonalInfo(userName: string) {
        return this.requestGetWithParams("api/Admin/GetPersonalInfo", { 'username': userName });
    }

    respondToConfirmation(userName: string, accept: boolean) {
        let params = { 'username': userName, 'accept': accept }
        return this.requestGetWithParams("api/Admin/RespondToConfirmation", params);
    }

    sortByField(fieldName: string, ascending: boolean) {
        let params = { 'fieldName': fieldName, 'ascending': ascending }
        return this.requestGetWithParams("api/Admin/SortByField", params);
    }

    blockUnblock(usersToBlock: string[]) {
        var params = { 'usersToBlock': usersToBlock };
        return this.requestGetWithParams("api/Admin/BlockUnblock", params);
    }
}