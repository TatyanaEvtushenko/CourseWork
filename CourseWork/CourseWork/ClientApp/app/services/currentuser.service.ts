import { Injectable, EventEmitter } from '@angular/core';
import { BaseService } from './base.service';
import { CurrentUser } from '../viewmodels/currentuser';

@Injectable()
export class CurrentUserService extends BaseService {
    isReady = new EventEmitter<CurrentUser>();

    getCurrentUser() {
        this.getCurrentUserFromServer().subscribe((data) => {
            this.changeServiceState(data);
        });
    }

    private changeServiceState(user: CurrentUser) {
        this.isReady.emit(user);
    }

    private getCurrentUserFromServer() {
        return this.requestGet("api/CurrentUser/GetCurrentUserInfo");
    }
}