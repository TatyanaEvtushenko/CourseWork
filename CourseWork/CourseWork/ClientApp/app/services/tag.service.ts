import { Injectable } from '@angular/core';
import {Http} from '@angular/http';

@Injectable()
export class TagService {

    constructor(private http: Http){ }

    getTagCloud() {
        return this.http.get("api/Tag/GetTagCloud");
    }
}