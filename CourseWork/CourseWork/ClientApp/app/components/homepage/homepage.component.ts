import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';

@Component({
    selector: 'homepage',
    templateUrl: './homepage.component.html'
})
export class HomePageComponent {
    constructor(title: Title) {
        title.setTitle("Home page");
    }
}
