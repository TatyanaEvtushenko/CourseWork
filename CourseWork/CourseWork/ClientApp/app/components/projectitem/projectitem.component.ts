import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'projectitem',
    templateUrl: './projectitem.component.html',
})

export class ProjectItemComponent {
    @Input() project: any;
    @Output() onClickNews = new EventEmitter<string>();

    openNews() {
        this.onClickNews.emit(this.project.id);
    }
}