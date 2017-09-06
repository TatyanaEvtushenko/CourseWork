import { Injectable } from '@angular/core';
import { BaseService} from './base.service';

@Injectable()
export class TagService extends BaseService{

    getTagCloud() {
        return this.requestGet("api/Tag/GetTagCloud");
    }
}