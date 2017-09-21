import { Component } from '@angular/core';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'searcher',
    templateUrl: './searcher.component.html'
})
export class SearcherComponent {
    keys = ["SEARCHPROJECTS"];
    translations = {}

    constructor(private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }
}