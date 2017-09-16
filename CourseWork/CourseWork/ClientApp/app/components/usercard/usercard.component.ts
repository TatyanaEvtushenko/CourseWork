import { Component, Input, Output, EventEmitter } from '@angular/core';
declare var $: any;

@Component({
    selector: 'usercard',
    templateUrl: './usercard.component.html',
})

export class UserCardComponent {
    @Input() displayableInfo: any;

    openAccountEdit() {
        $('#accountEditModal').modal("open");
    }

    openAvatarChange() {
        $("#avatarChangeModal").modal("open");
    }
}