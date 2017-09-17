import { Component, AfterViewInit, Input } from '@angular/core';
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

    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        $('#accountEditModal').modal();
    }

    onSubmit() {
        this.accountService.editAccount({ about: this.about, contacts: this.contacts }).subscribe((data: void) => $('#accountEditModal').modal("close"));
    }
}