import { Component } from '@angular/core';
import { StorageService } from '../../services/storage.service';
import { LocalizationService } from '../../services/localization.service';

@Component({
    selector: 'floatingbutton',
    templateUrl: './floatingbutton.component.html',
})
export class FloatingButtonComponent {
    keys = ["ConfirmYourAccount", "ADDANEWPROJECT"];
    translations = {}

    constructor(public storage: StorageService, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }
}