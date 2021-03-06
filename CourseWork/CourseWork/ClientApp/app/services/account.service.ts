﻿import { Injectable, EventEmitter } from '@angular/core';
import { BaseService} from './base.service';
import {RegisterForm} from '../viewmodels/registerform';
import { LoginForm } from '../viewmodels/loginform';
import { ConfirmationForm } from '../viewmodels/confirmationForm';
import { AccountEditForm } from "../viewmodels/accounteditform";

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

    sortByField(fieldName: string, ascending: boolean, filters: any) {
        let params = { 'fieldName': fieldName, 'ascending': ascending, 'confirmed': filters['confirmed'], 'unconfirmed': filters['unconfirmed'], 'requested': filters['requested'] }
        console.log(JSON.stringify(params));
        return this.requestGetWithParams("api/Admin/SortByField", params);
    }

    blockUnblock(usersToBlock: string[]) {
        var params = { 'usersToBlock': usersToBlock };
        return this.requestGetWithParams("api/Admin/BlockUnblock", params);
    }

    delete(usersToDelete: string[], withCommentsAndRaitings: boolean) {
        var params = { 'usersToDelete': usersToDelete, 'withCommentsAndRaitings': withCommentsAndRaitings };
        return this.requestGetWithParams("api/Admin/Delete", params);
    }

    getUserDisplayableInfo(username: string) {
        var params = { 'username': username };
        return this.requestGetWithParams("api/Account/GetUserDisplayableInfo", params);
    }

    getDisplayableInfo(userNames: string[]) {
        var params = { 'userNames': userNames };
        return this.requestGetWithParams("api/Account/GetDisplayableInfo", params);
    }

    editAccount(newInfo: AccountEditForm) {
        return this.requestPost("api/CurrentUser/Edit", newInfo);
    }

    changeAvatar(newAvatarB64: string) {
        return this.requestPost("api/CurrentUser/ChangeAvatar", newAvatarB64);
    }
}