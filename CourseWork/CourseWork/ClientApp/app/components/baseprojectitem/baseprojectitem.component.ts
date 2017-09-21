import { Component, Input } from '@angular/core';
import { ProjectStatus } from "../../enums/projectstatus";
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'baseprojectitem',
    templateUrl: './baseprojectitem.component.html',
})

export class BaseProjectItemComponent {
    @Input() project: any;
    projectStatus = ProjectStatus;
    keys = ["ABOUT", "ONE", "TWO", "THREE", "FOUR", "FIVE"];
    translations = {}

    constructor(private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }
}