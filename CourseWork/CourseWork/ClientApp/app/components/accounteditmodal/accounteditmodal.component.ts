import { Component, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { AccountService } from "../../services/account.service";
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

    constructor(private accountService: AccountService) { }

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