import { Component, AfterViewInit } from '@angular/core';
import { ConfirmationForm } from '../../viewmodels/confirmationform';
import { AccountService } from "../../services/account.service";
import { LocalizationService } from "../../services/localization.service";
declare var $: any;
declare var Materialize: any;

@Component({
    selector: 'confirmationmodal',
    templateUrl: './confirmationmodal.component.html'
})
export class ConfirmationModalComponent implements AfterViewInit {
    confirmationForm = new ConfirmationForm();
    isWrongRequest = false;
    keys = ["PASSPORTSCAN", "NAME", "SURNAME", "DESCRIPTION", "INVALIDDATA", "SEND", "ConfirmYourAccount", "CONFIRMATIONREQUEST",
        "CONFIRMATIONREQUESTSENT"];
    translations = {}

    constructor(private accountService: AccountService, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });}

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
            Materialize.toast(this.translations['CONFIRMATIONREQUESTSENT'], 4000);
        }
    }
}