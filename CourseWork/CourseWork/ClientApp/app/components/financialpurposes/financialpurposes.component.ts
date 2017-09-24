import { Component, Input, Output, AfterViewInit } from '@angular/core';
import { SortingHelper } from '../../helpers/sorting.helper';

@Component({
    selector: 'financialpurposes',
    templateUrl: './financialpurposes.component.html',
})

export class FinancialPurposesComponent implements AfterViewInit {
    @Input() canChange = false;
    @Input() @Output() financialPurposes: any;
    sortingHelper = new SortingHelper();

    ngAfterViewInit(): void {
        this.financialPurposes.sort(this.sortingHelper.sortByBudget);
    }

    delete(purpose: any) {
        const index = this.financialPurposes.indexOf(purpose);
        if (index >= 0) {
            this.financialPurposes.splice(index, 1);
        }
    }
}