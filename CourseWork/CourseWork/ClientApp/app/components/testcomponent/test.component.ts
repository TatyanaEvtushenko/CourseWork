import { Component, Input } from '@angular/core';

@Component({
    selector: 'test',
    templateUrl: './test.component.html'
})
export class TestComponent {
    @Input("numb") numb:string;
    n: number;
}