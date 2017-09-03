﻿import { CanActivate } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { CurrentUserService } from '../services/currentuser.service';

@Injectable()
export class ConfirmedUserActivator implements CanActivate {

    constructor(private currentUserService: CurrentUserService) { }

    canActivate(route: Object, state: Object): Observable<boolean> | Promise<boolean> | boolean {
        return this.currentUserService.isConfirmedUser();
    }
}