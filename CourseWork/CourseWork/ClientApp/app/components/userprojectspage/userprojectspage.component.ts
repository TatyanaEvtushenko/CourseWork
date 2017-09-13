import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent {
    projects: any[] = [];
    selectedProjectId: string = null;

    constructor(public storage: StorageService,
        private title: Title,
        private projectService: ProjectService) {
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