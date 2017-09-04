import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class BaseActivator implements CanActivate {

    constructor(private isInRole: Observable<boolean>, protected router: Router) { }

    canActivate(route: Object, state: Object): Observable<boolean> | Promise<boolean> | boolean {
        if (!this.isInRole) {
            this.router.navigate(['/dxtcfghujkl']);
        }
        return this.isInRole;
    }
}