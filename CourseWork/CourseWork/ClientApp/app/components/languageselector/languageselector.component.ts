import { Component } from '@angular/core';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'languageselector',
    templateUrl: './languageselector.component.html'
})
export class LanguageSelectorComponent {
    selectedLanguage: string;
    languages: any[] = [];

    constructor(private localizationService: LocalizationService){}

    ngOnInit() {
        this.localizationService.getSupportedLanguages().subscribe((data: any) => {
            this.selectedLanguage = data.currentLanguage;
            this.languages = data.supportedLanguages;
        });
    }

    languageSelected() {
        this.localizationService.setLanguage(this.selectedLanguage).subscribe(
            (data: void) => location.reload());
    }
}