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

	updateMessages() {
		return this.requestGet("api/Message/GetUnreadMessages");
	}

	markAsRead(messages: string[]) {
		var params = { 'id': messages };
		console.log(params);
		return this.requestGetWithParams("api/Message/MarkAsRead", params);
	}

    private changeServiceState(user: CurrentUser) {
        this.isReady.emit(user);
    }

    private getCurrentUserFromServer() {
        return this.requestGet("api/CurrentUser/GetCurrentUserInfo");
    }
}