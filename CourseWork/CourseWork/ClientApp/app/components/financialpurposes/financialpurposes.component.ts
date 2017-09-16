import { Component, Input, Output } from '@angular/core';
import { SortingService } from '../../services/sorting.service';

@Component({
    selector: 'financialpurposes',
    templateUrl: './financialpurposes.component.html',
})

export class FinancialPurposesComponent {
    @Input() canChange = false;
    @Input() @Output() financialPurposes: any;

    constructor(private sortingService: SortingService) { }

    ngOnInit() {
        this.financialPurposes.sort(this.sortingService.sortByBudget);
    }

    delete(purpose: any) {
        const index = this.financialPurposes.indexOf(purpose);
        if (index >= 0) {
            this.financialPurposes.splice(index, 1);
        }
    }
}