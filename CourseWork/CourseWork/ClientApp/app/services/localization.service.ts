import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class LocalizationService extends BaseService {
    getSupportedLanguages() {
        return this.requestGet("api/Localization/GetSupportedCultures");
    }

    setLanguage(cultureName: string) {
        return this.requestPost("api/Localization/SetLanguage", cultureName);
    }

    getTranslations(keys: string[]) {
        return this.requestGetWithParams("api/Localization/GetTranslations", { keys: keys });
    }
}