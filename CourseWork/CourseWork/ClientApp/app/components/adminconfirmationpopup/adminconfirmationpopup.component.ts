import { Component, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
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
    @Input() username: string;
    @Output() emitter = new EventEmitter<boolean>();

    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        this.accountService.getPersonalInfo(this.username).subscribe(userData => {
            this.userData = userData;
        });
        $('#adminConfirmationModal').modal();
    }

    accept() {
        this.respondToConfirmation(true);
    }

    decline() {
        this.respondToConfirmation(false);
    }

    private respondToConfirmation(accept: boolean) {
        this.accountService.respondToConfirmation(this.username, accept).subscribe((success) => {
            $('#adminConfirmationModal').modal("close");
            if (success) this.emitter.emit(accept);
        });
    }
}