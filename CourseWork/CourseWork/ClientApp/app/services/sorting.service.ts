import { Injectable } from '@angular/core';

@Injectable()
export class SortingService {

    sortByTime(a: any, b: any) {
        return this.sort(a.time, b.time, false);
    }

    sortByBudget(a: any, b: any) {
        return this.sort(a.budget, b.budget, false);
    }

    sortByProjectStatus(a: any, b: any) {
        return this.sort(a.status, b.status, false);
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