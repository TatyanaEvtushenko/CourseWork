import { Component, Input } from '@angular/core';
import { DisplayableInfo } from "../../viewmodels/displayableinfo";
declare var $: any;

@Component({
    selector: 'usercard',
    templateUrl: './usercard.component.html',
})

export class UserCardComponent {
    @Input() displayableInfo: DisplayableInfo;
    @Input() isCardOfCurrentUser: boolean;

    openAccountEdit() {
        $('#accountEditModal').modal("open");
    }

    openAvatarChange() {
        $("#avatarChangeModal").modal("open");
    }
}