import { Component, Input } from '@angular/core';

@Component({
    selector: 'errortext',
    templateUrl: './errortext.component.html',
})
export class ErrorTextComponent {
    @Input() text: string;
}