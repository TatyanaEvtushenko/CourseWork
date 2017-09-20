import { Component } from '@angular/core';
//import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'languageselector',
    templateUrl: './languageselector.component.html'
})
export class LanguageSelectorComponent {
    selectedLanguage: string = "en";
    languages: any[] = [{ name: "en", displayName: "en" }, { name: "ru", displayName: "ru" }];

    //constructor(/*private localizationService: LocalizationService*/){}

    ngOnInit() {
        //this.localizationService.getSupportedLanguages().subscribe();
    }

    languageSelected(event: any) {
        console.log("Selected " + event);
    }
}