import { Injectable } from '@angular/core';
import { BaseService} from './base.service';

@Injectable()
export class CurrentUserService extends BaseService{

    getCurrentUserInfo() {
        return this.requestGet("api/CurrentUser/GetCurrentUserInfo");
    }

    isAdmin() {
        return this.requestGet("api/Account/IsAdmin");
    }

    isConfirmedUser() {
        return this.requestGet("api/Account/IsConfirmedUser");
    }

    isUser() {
        return this.requestGet("api/Account/IsUser");
    }
}