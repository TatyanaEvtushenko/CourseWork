import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ColorService extends BaseService {
    getSupportedColors() {
        return this.requestGet("api/Colors/GetSupportedColors");
    }

    setColor(colorName: string) {
        return this.requestPost("api/Colors/SetColor", colorName);
    }
}