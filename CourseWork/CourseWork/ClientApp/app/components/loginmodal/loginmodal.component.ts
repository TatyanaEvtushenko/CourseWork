import { Component, AfterViewInit } from '@angular/core';
import {LoginForm} from '../../viewmodels/loginform';
import { AccountService } from "../../services/account.service";
declare var $: any;

@Component({
    selector: 'loginmodal',
    templateUrl: './loginmodal.component.html'
})
export class LoginModalComponent implements AfterViewInit {
    loginForm = new LoginForm();
    isWrongRequest = false;

    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        $('#loginModal').modal();
    }

    onSubmit() {
        this.accountService.login(this.loginForm).subscribe(
            (data) => {
                this.isWrongRequest = !data;
                if (!this.isWrongRequest) {
                    this.accountService.changeAuthState(true);
                }
            },
            (error) => this.isWrongRequest = true
        );
    }
}