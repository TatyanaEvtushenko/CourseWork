import { Component, AfterViewInit, Input } from '@angular/core';
import { AccountService } from "../../services/account.service";
declare var $: any;

@Component({
    selector: 'accounteditmodal',
    templateUrl: './accounteditmodal.component.html'
})
export class AccountEditModalComponent implements AfterViewInit {
    @Input() about: string;

    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        $('#accountEditModal').modal();
    }

    onSubmit() {
        this.accountService.editAccount(this.about).subscribe((data: void) => $('#accountEditModal').modal("close"));
    }
}