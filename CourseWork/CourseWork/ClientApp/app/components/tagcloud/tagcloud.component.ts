import { Component, Input } from '@angular/core';

@Component({
    selector: 'tagcloud',
    templateUrl: './tagcloud.component.html'
})
export class TagCloudComponent {
    words: Object[];

    constructor() {
        this.words = [
            {text: "Lorem", weight: 13, link: 'http://github.com/mistic100/jQCloud'},
            {text: "Ipsum", weight: 10.5, link: 'http://www.strangeplanet.fr'},
            {text: "Dolor", weight: 9.4, link: 'http://piwigo.org'},
        ];
    }
}