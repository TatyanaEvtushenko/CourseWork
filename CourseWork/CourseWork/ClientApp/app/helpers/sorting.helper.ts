import { FinancialPurpose } from '../viewmodels/financialpurpose';

export class SortingHelper {

    sortByTimeDescending(a: any, b: any) {
        if (a.time > b.time) {
            return -1;
        }
        if (a.time === b.time) {
            return 0;
        } else {
            return 1;
        };
    }

    sortByBudget(a: FinancialPurpose, b: FinancialPurpose) {
        if (a.budget > b.budget) {
            return 1;
        }
        if (a.budget === b.budget) {
            return 0;
        } else {
            return -1;
        };
    }

    sortByProjectStatus(a: any, b: any) {
        if (a.status > b.status) {
            return 1;
        }
        if (a.status === b.status) {
            return 0;
        } else {
            return -1;
        };
    }
}