import { Component } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { SortingHelper } from '../../helpers/sorting.helper';
import { TimeHelper } from '../../helpers/time.helper';
import { ColorPickerService } from 'ngx-color-picker';

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent{
    projectForm = new NewProjectForm();
    isWrongRequest = false;
    sortingHelper = new SortingHelper();
    timeHelper = new TimeHelper();

    constructor(public storage: StorageService,
        private title: Title,
        private projectService: ProjectService,
        private cpService: ColorPickerService) {
        title.setTitle("New project");
        this.initializeProject();
    }

    addFinancialPurpose(purpose: any) {
        this.projectForm.financialPurposes.push(purpose);
        this.projectForm.financialPurposes.sort(this.sortingHelper.sortByBudget);
    }

    onSubmit() {
        this.projectService.addProject(this.projectForm).subscribe(
            (data) => this.getResponse(data),
            (error) => this.isWrongRequest = true
        );
    }

    private getResponse(data: any) {
        this.isWrongRequest = !data;
        if (!this.isWrongRequest) {
            window.location.href = `/ProjectPage/${this.projectForm.id}`;
        }
    }

    private initializeProject() {
        this.projectForm.financialPurposes = [];
        this.projectForm.tags = [];
    }
}