import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'usercard',
    templateUrl: './usercard.component.html',
})

export class UserCardComponent {
    @Input() displayableInfo: any;
    @Output() onClickEdit = new EventEmitter<string>();

    openEdit() {
        this.onClickEdit.emit('');
    }
}