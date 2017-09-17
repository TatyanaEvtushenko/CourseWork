import { Component, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { AccountService } from "../../services/account.service";
declare var $: any;

@Component({
    selector: 'avatarchangemodal',
    templateUrl: './avatarchangemodal.component.html'
})
export class AvatarChangeModalComponent implements AfterViewInit {
    avatarB64: string = "";
    @Output() onConfirm = new EventEmitter<string>();

    constructor(private accountService: AccountService) { }

    ngAfterViewInit() {
        $('#avatarChangeModal').modal();
    }

    onSubmit() {
        this.accountService.changeAvatar(this.avatarB64).subscribe((data: any) => {
            this.onConfirm.emit(data.value);
            $('#avatarChangeModal').modal("close");
        });
    }
}