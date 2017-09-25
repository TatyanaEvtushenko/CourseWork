import { Component, Input } from '@angular/core';
import { DisplayableInfo } from "../../viewmodels/displayableinfo";
import { LocalizationService } from "../../services/localization.service";
import { TimeHelper } from '../../helpers/time.helper';
declare var $: any;

@Component({
    selector: 'usercard',
    templateUrl: './usercard.component.html',
})

export class UserCardComponent {
    @Input() displayableInfo: DisplayableInfo;
    @Input() isCardOfCurrentUser: boolean;

    timeHelper = new TimeHelper(this.localizationService);
    keys = ["ABOUT", "REGISTRATIONTIME", "PROJECTNUMBER", "CONTACTS", "ONE", "TWO", "THREE", "FOUR", "FIVE"];
    translations = {}

    constructor(private localizationService: LocalizationService) {
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