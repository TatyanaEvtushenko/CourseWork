import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { CurrentUserService } from '../services/currentuser.service';
import { BaseActivator } from './base.activator';

@Injectable()
export class ConfirmedUserActivator extends BaseActivator {

    constructor(private currentUserService: CurrentUserService, protected router: Router) {
        super(currentUserService.isConfirmedUser(), router);
    }
}