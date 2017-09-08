import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { ConfirmationForm } from '../../viewmodels/confirmationform';
import { AccountService } from "../../services/account.service";
declare var $: any;
declare var Materialize: any;

@Component({
    selector: 'adminconfirmationpopup',
    templateUrl: './adminconfirmationpopup.component.html'
})
export class AdminConfirmationPopupComponent implements AfterViewInit {
    userData = new ConfirmationForm();
    username = "";

    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        this.username = $('#adminConfirmationModal').data('username');
        console.log(this.username);
        $('#adminConfirmationModal').modal();
    }

    accept() {
        this.respondToConfirmation(true);
    }

    decline() {
        this.respondToConfirmation(false);
    }

    display() {
        $('#adminConfirmationModal').modal();
    }

    private respondToConfirmation(accept: boolean) {
        this.accountService.respondToConfirmation("2", accept);
    }
}