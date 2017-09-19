import { Component } from '@angular/core';

@Component({
    selector: 'languageselector',
    templateUrl: './languageselector.component.html'
})
export class LanguageSelectorComponent {
    selectedLanguage: string = "en";
    languages: any[] = [{ name: "en", displayName: "en" }, { name: "ru", displayName: "ru" }];

    languageSelected(event: any) {
        console.log("Selected " + event);
    }
}