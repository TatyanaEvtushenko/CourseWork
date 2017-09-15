import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ProjectStatus } from "../../enums/projectstatus";

@Component({
    selector: 'baseprojectitem',
    templateUrl: './baseprojectitem.component.html',
})

export class BaseProjectItemComponent {
    @Input() project: any;
    projectStatus = ProjectStatus;
}