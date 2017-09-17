import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class CurrentUserService extends BaseService {

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
		return this.requestGetWithParams("api/Message/MarkAsRead", params);
	}

    private changeServiceState(user: CurrentUser) {
        this.isReady.emit(user);
    }

    private getCurrentUserFromServer() {
        return this.requestGet("api/CurrentUser/GetCurrentUserInfo");
    }
}