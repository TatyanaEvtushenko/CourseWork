import { Component, Input, Output } from '@angular/core';

@Component({
    selector: 'financialpurposes',
    templateUrl: './financialpurposes.component.html',
})

export class FinancialPurposesComponent {
    @Input() canChange = false;
    @Input() @Output() financialPurposes: any;

    delete(purpose: any) {
        const index = this.financialPurposes.indexOf(purpose);
        if (index >= 0) {
            this.financialPurposes.splice(index, 1);
        }
    }
}