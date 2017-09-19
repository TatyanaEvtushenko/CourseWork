import { Component } from '@angular/core';
import { StorageService } from '../../services/storage.service';

@Component({
    selector: 'floatingbutton',
    templateUrl: './floatingbutton.component.html',
})
export class FloatingButtonComponent {
    constructor(public storage: StorageService) { }
}