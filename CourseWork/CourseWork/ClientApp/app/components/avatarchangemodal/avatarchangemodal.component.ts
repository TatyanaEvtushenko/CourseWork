import { Component, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { AccountService } from "../../services/account.service";
import { LocalizationService } from "../../services/localization.service";
declare var $: any;

@Component({
    selector: 'avatarchangemodal',
    templateUrl: './avatarchangemodal.component.html'
})
export class AvatarChangeModalComponent implements AfterViewInit {
    avatarB64: string = "";
    @Output() onConfirm = new EventEmitter<string>();
    keys = ["APPLY", "CHANGEAVATAR"];
    translations = {};

    constructor(private accountService: AccountService, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

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