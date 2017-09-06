import { Component, AfterViewInit } from '@angular/core';
import { ConfirmationForm } from '../../viewmodels/confirmationform';
import { ConfirmationFormImage } from '../../viewmodels/confirmationformimage';
import { AccountService } from "../../services/account.service";
declare var $: any;
declare var Materialize: any;

@Component({
    selector: 'confirmationmodal',
    templateUrl: './confirmationmodal.component.html'
})
export class ConfirmationModalComponent implements AfterViewInit {
    confirmationForm = new ConfirmationForm();
    confirmationFormImage = new ConfirmationFormImage();
    imageString = "";
    isWrongRequest = false;

    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        $('#confirmationModal').modal();
    }

    onChange(event) {
        this.imageString = event;
    }

    onSubmit() {
        this.confirmationForm.PassportScan = this.imageString;
        this.accountService.confirmAccount(this.confirmationForm).subscribe(
            (data) => {
                this.isWrongRequest = !data;
                if (!this.isWrongRequest) {
                    $('#confirmationModal').modal("close");
                    Materialize.toast('Confirmation request has been sent to admin.', 4000);
                }
            },
            (error) => this.isWrongRequest = true
        );
    }
}