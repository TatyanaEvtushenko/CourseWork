import { Component, Input } from '@angular/core';
import { StorageService } from '../../services/storage.service';

@Component({
    selector: 'usermininfo',
    templateUrl: './usermininfo.component.html'
})

export class UserMinInfoComponent {
    @Input() user: any;

    constructor(public storage: StorageService) { }
}