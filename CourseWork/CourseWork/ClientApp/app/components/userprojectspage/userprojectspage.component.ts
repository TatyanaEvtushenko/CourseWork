import { Component, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { SortingService } from '../../services/sorting.service';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent {
    @Input() projects: any[] = [];
    selectedProjectId: string = null;

    constructor(public storage: StorageService,
        private title: Title,
        private sortingService: SortingService,
        private projectService: ProjectService) {
        title.setTitle("My projects");
    }

    ngOnInit() {
        this.projectService.getUserProjects().subscribe(
            (data) => {
                data.sort(this.sortingService.sortByProjectStatus);
                this.projects = data;
            }
        );
    }
}