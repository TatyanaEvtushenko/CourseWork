import { Component, AfterViewInit } from '@angular/core';
import {LoginForm} from '../../viewmodels/loginform';
import { AccountService } from "../../services/account.service";
import { LocalizationService } from "../../services/localization.service";
declare var $: any;

@Component({
    selector: 'loginmodal',
    templateUrl: './loginmodal.component.html'
})
export class LoginModalComponent implements AfterViewInit {
    loginForm = new LoginForm();
    isWrongRequest = false;
    keys = ["EMAIL", "PASSWORD", "LOGINERROR", "REGISTERASNEW", "LogIn"];
    translations = {}

    constructor(private accountService: AccountService, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    ngAfterViewInit() {
        $('#loginModal').modal();
        $('#login-password').characterCounter();
    }

    onSubmit() {
        if (this.loginForm.email != null && this.loginForm.password != null) {
            this.accountService.login(this.loginForm).subscribe(
                (data) => this.getResponse(data),
                (error) => this.isWrongRequest = true
            );
        }
    }

    private getResponse(data: any) {
        this.isWrongRequest = !data;
        if (!this.isWrongRequest) {
            $('#loginModal').modal("close");
            this.accountService.changeAuthState(true);
        }
    }
}