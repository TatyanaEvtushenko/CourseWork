import { Injectable } from '@angular/core';
import { BaseService} from './base.service';

@Injectable()
export class CurrentUserService extends BaseService{

    getCurrentUserInfo() {
        return this.requestGet("api/CurrentUser/GetCurrentUserInfo");
    }
}