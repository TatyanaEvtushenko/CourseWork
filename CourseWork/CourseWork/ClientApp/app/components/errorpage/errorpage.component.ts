import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';

@Component({
    selector: 'errorpage',
    templateUrl: './errorpage.component.html'
})
export class ErrorPageComponent {
    constructor(title: Title) {
        title.setTitle("Sorry...");
    }
}
