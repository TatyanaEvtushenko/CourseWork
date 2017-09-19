import { Component } from '@angular/core';
import { StorageService } from '../../services/storage.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent {
    constructor(public storage: StorageService){ }
 }