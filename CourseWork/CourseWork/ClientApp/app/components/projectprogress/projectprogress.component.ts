import { Component, Input } from '@angular/core';
declare var $: any;

@Component({
    selector: 'projectprogress',
    templateUrl: './projectprogress.component.html',
})

export class ProjectProgressComponent {
    @Input() neccessaryAmount: number;
    @Input() paidAmount: number;

    getWidth() {
        const percentage = this.neccessaryAmount === 0
            ? 0
            : (this.paidAmount >= this.neccessaryAmount ? 100 : this.paidAmount / this.neccessaryAmount * 100);
        return percentage + "%";
    }
}