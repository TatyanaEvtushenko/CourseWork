import { Component, AfterViewInit } from '@angular/core';
import { ConfirmationForm } from '../../viewmodels/confirmationform';
import { AccountService } from "../../services/account.service";
declare var $: any;
declare var Materialize: any;

@Component({
    selector: 'confirmationmodal',
    templateUrl: './confirmationmodal.component.html'
})
export class ConfirmationModalComponent implements AfterViewInit {
    confirmationForm = new ConfirmationForm();
    isWrongRequest = false;

    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        $('#confirmationModal').modal();
    }

    onChange(event: any) {
        this.confirmationForm.passportScan = event;
    }

    onSubmit() {
        if (this.confirmationForm.name != null && this.confirmationForm.surname != null && this.confirmationForm.passportScan != null) {
            this.accountService.confirmAccount(this.confirmationForm).subscribe(
                (data) => this.getResponse(data),
                (error) => this.isWrongRequest = true
            );
        }
    }

    private getResponse(data: any) {
        this.isWrongRequest = !data;
        if (!this.isWrongRequest) {
            $('#confirmationModal').modal("close");
            Materialize.toast('Confirmation request has been sent to admin.', 4000);
        }
    }
}