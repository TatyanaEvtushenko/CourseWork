import { Component } from '@angular/core';
import { LocalizationService } from "../../services/localization.service";
import { ColorService } from "../../services/color.service";

@Component({
    selector: 'colorselector',
    templateUrl: './colorselector.component.html'
})
export class ColorSelectorComponent {
    selectedColor: string;
    colors: string[] = [];
    keys = ['CHANGECOLOR', "LIGHT", "DARK"];
    translations = {};

    constructor(private localizationService: LocalizationService, private colorService: ColorService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
            this.colorService.getSupportedColors().subscribe((data: any) => {
                this.selectedColor = data.currentColor;
                this.colors = data.supportedColors;
            });
        });
    }

    colorSelected() {
        this.colorService.setColor(this.selectedColor).subscribe(
            (data: void) => location.reload());
    }
}