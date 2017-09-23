import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DisplayableInfo } from "../../viewmodels/displayableinfo";
import { AccountEditForm } from '../../viewmodels/accounteditform'
import { LocalizationService } from "../../services/localization.service";
import { TimeService } from "../../services/time.service";
declare var $: any;

@Component({
    selector: 'usercard',
    templateUrl: './usercard.component.html',
})

export class UserCardComponent {
    @Input() displayableInfo: DisplayableInfo;
    @Input() isCardOfCurrentUser: boolean;

    keys = ["ABOUT", "REGISTRATIONTIME", "PROJECTNUMBER", "CONTACTS", "ONE", "TWO", "THREE", "FOUR", "FIVE"];
    translations = {}

    constructor(private localizationService: LocalizationService, private timeService: TimeService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    openAccountEdit() {
        $('#accountEditModal').modal("open");
    }

    openAvatarChange() {
        $("#avatarChangeModal").modal("open");
    }
}