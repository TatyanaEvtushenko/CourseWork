import { Component, Input } from '@angular/core';
import { ProjectStatus } from "../../enums/projectstatus";
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'projectstatus',
    templateUrl: './projectstatus.component.html',
})

export class ProjectStatusComponent {
    @Input() status: ProjectStatus;
    projectStatus = ProjectStatus;
    keys = ["ACTIVE", "FINANCED", "FAILED"];
    translations = {}

    constructor(private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }
}