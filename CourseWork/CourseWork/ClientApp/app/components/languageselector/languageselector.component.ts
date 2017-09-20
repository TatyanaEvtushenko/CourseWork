import { Component } from '@angular/core';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'languageselector',
    templateUrl: './languageselector.component.html'
})
export class LanguageSelectorComponent {
    selectedLanguage: string = "en";
    languages: any[] = [{ name: "en", displayName: "en" }, { name: "ru", displayName: "ru" }];

    constructor(private localizationService: LocalizationService){}

    ngOnInit() {
        this.localizationService.getSupportedLanguages().subscribe((data: any) => {
            console.log(JSON.stringify(data));
            this.selectedLanguage = data.currentLanguage;
            this.languages = data.supportedLanguages;
        });
    }

    languageSelected() {
        console.log("Selected " + JSON.stringify(this.selectedLanguage));
    }
}