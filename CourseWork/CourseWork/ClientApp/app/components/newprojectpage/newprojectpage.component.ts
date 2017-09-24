import { Component } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { SortingHelper } from '../../helpers/sorting.helper';
declare var $: any;

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent{
    projectForm = new NewProjectForm();
    isWrongRequest = false;
    sortingHelper = new SortingHelper();

    constructor(public storage: StorageService,
                private title: Title, 
                private projectService: ProjectService) {
        title.setTitle("New project");
        this.projectForm.financialPurposes = [];
        this.projectForm.tags = [];
    }

    getTodayDate() {
        return new Date(Date.now()).getDate();
    }

    addFinancialPurpose(purpose: any) {
        this.projectForm.financialPurposes.push(purpose);
        this.projectForm.financialPurposes.sort(this.sortingHelper.sortByBudget);
    }

    onSubmit() {
        this.projectService.addProject(this.projectForm).subscribe(
            (data) => {
                this.isWrongRequest = !data;
                if (!this.isWrongRequest) {
                    window.location.href = "/UserPage";
                }
            },
            (error) => this.isWrongRequest = true
        );
    }
}