import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent {
    constructor(title: Title) {
        title.setTitle("My projcts");
    }
}