import { Component, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { AccountService } from "../../services/account.service";
import { LocalizationService } from "../../services/localization.service";
import { AccountEditForm } from '../../viewmodels/accounteditform'

declare var $: any;

@Component({
    selector: 'accounteditmodal',
    templateUrl: './accounteditmodal.component.html'
})
export class AccountEditModalComponent implements AfterViewInit {
    @Input() about: string;
    @Input() contacts: string;
    @Output() onConfirm = new EventEmitter<AccountEditForm>();
    keys = ["EDITACCOUNT", "CONTACTS", "ABOUT", "APPLY"];
    translations = {}

    constructor(private accountService: AccountService, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    ngAfterViewInit() {
        $('#accountEditModal').modal();
    }

    onSubmit() {
        this.accountService.editAccount({ about: this.about, contacts: this.contacts }).subscribe((data: void) => {
            this.onConfirm.emit({ about: this.about, contacts: this.contacts });
            $('#accountEditModal').modal("close");
        });
    }
}