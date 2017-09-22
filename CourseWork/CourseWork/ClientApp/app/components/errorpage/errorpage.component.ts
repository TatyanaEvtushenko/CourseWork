import { Component } from '@angular/core';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'errorpage',
    templateUrl: './errorpage.component.html'
})
export class ErrorPageComponent {
    keys = ["PAGENOTFOUND"];
    translations = {}

    constructor(private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }
}
