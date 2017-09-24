import { Component, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { SortingHelper } from '../../helpers/sorting.helper';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent {
    @Input() projects: any[] = [];
    selectedProjectId: string = null;
    sortingHelper = new SortingHelper();

    constructor(public storage: StorageService,
        private title: Title,
        private projectService: ProjectService) {
        title.setTitle("My projects");
    }

    ngOnInit() {
        this.projectService.getUserProjects().subscribe(
            (data: any) => {
                data.sort(this.sortingHelper.sortByProjectStatus);
                this.projects = data;
            }
        );
    }
}