import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';

@Component({
    selector: 'projectpage',
    templateUrl: './projectpage.component.html'
})
export class ProjectPageComponent {
    projects: any[] = [];
    selectedProjectId: string = null;

    constructor(private title: Title, protected projectService: ProjectService) {
        title.setTitle("My projects");
    }

    ngOnInit() {
        this.projectService.getUserProjects().subscribe(
            (data) => {
                this.projects = data;
                this.projects.sort((a, b) => {
                    if (a.status > b.status) {
                        return 1;
                    }
                    if (a.status === b.status) {
                        return 0;
                    } else {
                        return -1;
                    }
                });
            }
        );
    }

    openNewsModal(event: any) {
        this.selectedProjectId = event;
    }
}