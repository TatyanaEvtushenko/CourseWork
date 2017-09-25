import { Component, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { SortingHelper } from '../../helpers/sorting.helper';
import { LocalizationService } from '../../services/localization.service';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent {
    @Input() projects: any[] = [];
    selectedProjectId: string = null;
    sortingHelper = new SortingHelper();
    keys = ['MYPROJECTS', ''];
    translations = {};

    constructor(public storage: StorageService,
        private title: Title,
        private projectService: ProjectService,
        private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe(data => {
            this.translations = data;
            title.setTitle(this.translations['MYPROJECTS']);
        });
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