import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ProjectStatus } from "../../enums/projectstatus";

@Component({
    selector: 'projectitem',
    templateUrl: './projectitem.component.html',
})

export class ProjectItemComponent {
    @Input() project: any;
    @Output() onClickNews = new EventEmitter<string>();
    projectStatus = ProjectStatus;

    openNews() {
        this.onClickNews.emit(this.project.id);
    }
}