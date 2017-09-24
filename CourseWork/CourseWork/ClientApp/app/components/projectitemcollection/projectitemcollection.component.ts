import { Component, Input, Output, EventEmitter } from '@angular/core';
import { StorageService } from '../../services/storage.service';
import { ProjectService } from '../../services/project.service';

@Component({
    selector: 'projectitemcollection',
    templateUrl: './projectitemcollection.component.html',
})
export class ProjectItemCollectionComponent {
    @Input() projects: any;
    @Output() selectedProjectIdNews = new EventEmitter<string>();
    @Output() selectedProjectIdPayment = new EventEmitter<string>();

    constructor(public storage: StorageService, private projectService: ProjectService) {}

    openPayment(event: any) {
        this.selectedProjectIdPayment.emit(event);
    }

    openNewsModal(event: any) {
        this.selectedProjectIdNews.emit(event);
    }

    subscribe(projectId: string) {
        this.projectService.subscribe(projectId).subscribe();
    }

    unsubscribe(projectId: string) {
        this.projectService.unsubscribe(projectId).subscribe();
    }

}