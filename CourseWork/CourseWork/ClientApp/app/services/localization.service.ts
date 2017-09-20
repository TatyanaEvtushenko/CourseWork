import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class LocalizationService extends BaseService {
    getSupportedLanguages() {
        return this.requestGet("api/Localization/GetSupportedCultures");
    }
}