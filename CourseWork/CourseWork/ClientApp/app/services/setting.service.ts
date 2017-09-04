import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class SettingService extends BaseService {

    getRoleNames() {
        return this.requestGet("api/Setting/GetAllRoles");
    }
}