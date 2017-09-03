import { Component, AfterViewInit } from '@angular/core';
import {RegisterForm} from '../../viewmodels/registerform';
import { AccountService } from "../../services/account.service";
declare var $: any;

@Component({
    selector: 'registermodal',
    templateUrl: './registermodal.component.html'
})
export class RegisterModalComponent implements AfterViewInit {
    registerForm: RegisterForm = new RegisterForm();
    isValidPassword: boolean = false;
    isValidPasswordConfirmation: boolean = false;
     
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
        this.accountService.register(this.registerForm).subscribe((response: Response) => {
            alert(response.json());
        });
    }
}