import { Injectable } from '@angular/core';

@Injectable()
export class SortingService {

    sortByTime(a: any, b: any) {
        if (a.time > b.time) {
            return -1;
        }
        if (a.time === b.time) {
            return 0;
        }
        return 1;
    }

    sortByBudget(a: any, b: any) {
        if (a.budget > b.budget) {
            return -1;
        }
        if (a.budget === b.budget) {
            return 0;
        }
        return 1;
    }

    sortByProjectStatus(a: any, b: any) {
        if (a.status > b.status) {
            return 1;
        }
        if (a.status === b.status) {
            return 0;
        } else {
            return -1;
        }
    }
}