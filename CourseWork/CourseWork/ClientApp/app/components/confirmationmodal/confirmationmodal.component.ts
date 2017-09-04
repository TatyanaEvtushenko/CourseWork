import { Component, AfterViewInit } from '@angular/core';
import { ConfirmationForm } from '../../viewmodels/confirmationform';
import { UnconfirmedUserService } from "../../services/unconfirmedUser.service";
declare var $: any;

@Component({
    selector: 'confirmationmodal',
    templateUrl: './confirmationmodal.component.html'
})
export class ConfirmationModalComponent implements AfterViewInit {
    confirmationForm = new ConfirmationForm();
    isWrongRequest = false;

    constructor(private unconfirmedUserService: UnconfirmedUserService) { }

    ngAfterViewInit() {
        $('#confirmationModal').modal();
    }

    onSubmit() {
        this.unconfirmedUserService.confirmAccount(this.confirmationForm).subscribe(
            (data) => {
                this.isWrongRequest = !data;
                if (!this.isWrongRequest) {
                    $('#confirmationModal').modal("close");
                }
            },
            (error) => this.isWrongRequest = true
        );
    }
}