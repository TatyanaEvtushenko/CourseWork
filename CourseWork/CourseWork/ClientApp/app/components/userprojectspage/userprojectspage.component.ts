import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent {
    projects: any[] = [];
    selectedProjectId: string = null;

    constructor(private title: Title, protected projectService: ProjectService) {
        title.setTitle("My projects");
    }

    ngOnInit() {
        this.projectService.getUserProjects().subscribe(
            (data) => this.projects = data
        );
    }

    openNewsModal(event: any) {
        this.selectedProjectId = event;
    }
}