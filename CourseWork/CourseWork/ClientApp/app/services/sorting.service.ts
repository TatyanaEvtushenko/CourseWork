import { Injectable } from '@angular/core';
import { FinancialPurpose } from '../viewmodels/financialpurpose';

@Injectable()
export class SortingService {

    sortByTime(a: any, b: any) {
        return (a: any, b: any) => this.sort(a.time, b.time, true);
    }

    sortByBudget(a: FinancialPurpose, b: FinancialPurpose) {
        return (a: FinancialPurpose, b: FinancialPurpose) => this.sort(a.budget, b.budget, false);
    }

    sortByProjectStatus(a: any, b: any) {
        return (a: any, b: any) => this.sort(a.status, b.status, false);
    }

    private sort(firstItem: any, secondItem: any, isDescending: boolean) {
        if (firstItem > secondItem) {
            return isDescending ? -1 : 1;
        }
        if (firstItem === secondItem) {
            return 0;
        } else {
            return isDescending ? 1 : -1;
        }
    }
}