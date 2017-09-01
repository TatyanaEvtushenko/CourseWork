import { Component } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html'
})
export class AppComponent {
    numb:number;
    numb1:number;

    constructor() {
        this.numb = 10;
        this.numb1 = 5612574;
    }
}