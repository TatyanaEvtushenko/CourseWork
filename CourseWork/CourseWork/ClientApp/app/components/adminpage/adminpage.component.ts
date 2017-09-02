import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';

@Component({
    selector: 'adminpage',
    templateUrl: './adminpage.component.html'
})
export class AdminPageComponent {
    constructor(title: Title) {
        title.setTitle("Admin page");
    }
}