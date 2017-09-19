import { Component, Input } from '@angular/core';
import { ProjectStatus } from "../../enums/projectstatus";

@Component({
    selector: 'projectstatus',
    templateUrl: './projectstatus.component.html',
})

export class ProjectStatusComponent {
    @Input() status: ProjectStatus;
    projectStatus = ProjectStatus;
}