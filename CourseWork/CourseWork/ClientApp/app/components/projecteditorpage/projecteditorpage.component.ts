import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { SortingService } from '../../services/sorting.service';
import { NewProjectForm } from '../../viewmodels/newprojectform';
declare var $: any;

@Component({
    selector: 'projecteditorpage',
    templateUrl: './projecteditorpage.component.html',
})

export class ProjectEditorPageComponent {
    project: NewProjectForm = null;
    isWrongRequest = false;

    constructor(public storage: StorageService,
        private title: Title,
        private projectService: ProjectService,
        private route: ActivatedRoute,
        private sortingService: SortingService) {
        title.setTitle("Edit project");
    }

    ngOnInit() {
        const request = this.route.paramMap.switchMap((params: ParamMap) =>
            this.projectService.getProjectEditorForm(params.get('id')));
        request.subscribe(data => this.prepareData(data));
    }

    getTodayDate() {
        return new Date(Date.now()).getDate();
    }

    addFinancialPurpose(purpose: any) {
        this.project.financialPurposes.push(purpose);
    }

    deleteFinancialPurpose(purpose: any) {
        const index = this.project.financialPurposes.indexOf(purpose);
        if (index >= 0) {
            this.project.financialPurposes.splice(index, 1);
        }
    }

    onSubmit() {
        console.log(this.project);
        this.projectService.updateProject(this.project).subscribe(
            (data) => {
                this.isWrongRequest = !data;
                if (!this.isWrongRequest) {
                    window.location.href = `/ProjectsPage/${this.project.id}`;
                }
            },
            (error) => this.isWrongRequest = true
        );
    }

    private prepareData(data: any) {
        data.financialPurposes.sort(this.sortingService.sortByBudget);
        this.project = data;
        this.project.imageBase64 = data.imageUrl;
    }
}