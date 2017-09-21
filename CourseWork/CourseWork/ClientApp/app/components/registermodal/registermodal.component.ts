import { Component, AfterViewInit } from '@angular/core';
import {RegisterForm} from '../../viewmodels/registerform';
import { AccountService } from "../../services/account.service";
import { LocalizationService } from "../../services/localization.service";
declare var $: any;
declare var Materialize: any;

@Component({
    selector: 'registermodal',
    templateUrl: './registermodal.component.html'
})
export class RegisterModalComponent implements AfterViewInit {
    registerForm = new RegisterForm();
    isValidPassword = false;
    isValidPasswordConfirmation = false;
    isWrongRequest = false;
    keys = ["CREATEACCOUNT", "EMAIL", "PASSWORD", "PASSWORDCONFIRM", "USERNAME", "ERRORPASSWORDLENGTH", "ERRORCONFIRMPASSWORD",
        "ERRORALREADYEXISTS", "Register"];
    translations = {}

    constructor(private accountService: AccountService, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    ngAfterViewInit() {
        $('#registrationModal').modal();
        $('#register-confirmPassword, #register-password').characterCounter();
    }

    onPasswordChange() {
        this.isValidPassword = this.registerForm.password.length >= 6;
    }

    onPasswordConfirmationChange() {
        this.isValidPasswordConfirmation = this.registerForm.password == this.registerForm.passwordConfirmation;
    }

    onSubmit() {
        this.accountService.register(this.registerForm).subscribe(
            (data) => this.getResponse(data),
            (error) => this.isWrongRequest = true
        );
    }

    private getResponse(data: any) {
        this.isWrongRequest = !data;
        if (!this.isWrongRequest) {
            $('#registrationModal').modal("close");
            Materialize.toast('Confirmation is sent.', 4000);
        }
    }
}