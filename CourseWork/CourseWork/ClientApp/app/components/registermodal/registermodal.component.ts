import { Component, AfterViewInit } from '@angular/core';
import {RegisterForm} from '../../viewmodels/registerform';
import { AccountService } from "../../services/account.service";
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
     
    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        $('#registrationModal').modal();
    }

    onPasswordChange() {
        this.isValidPassword = this.registerForm.password.length >= 6;
    }

    onPasswordConfirmationChange() {
        this.isValidPasswordConfirmation = this.registerForm.password == this.registerForm.passwordConfirmation;
    }

    onSubmit() {
        this.accountService.register(this.registerForm).subscribe(
            (data) => {
                this.isWrongRequest = !data;
                if (!this.isWrongRequest) {
                    $('#registrationModal').modal("close");
                    Materialize.toast('Confirmation is sent.', 4000);
                }
            },
            (error) => this.isWrongRequest = true
        );
    }
}