import { Component } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
declare var $: any;

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent{
    projectForm = new NewProjectForm();
    isWrongRequest = false;

    constructor(public storage: StorageService,
                private title: Title, 
                private projectService: ProjectService) {
        title.setTitle("New project");
        this.projectForm.financialPurposes = [];
    }

    getTodayDate() {
        return new Date(Date.now()).getDate();
    }

    addFinancialPurpose(purpose: any) {
        this.projectForm.financialPurposes.push(purpose);
    }

    deleteFinancialPurpose(purpose: any) {
        const index = this.projectForm.financialPurposes.indexOf(purpose);
        if (index >= 0) {
            this.projectForm.financialPurposes.splice(index, 1);
        }
    }

    onSubmit() {
        this.projectService.addProject(this.projectForm).subscribe(
            (data) => {
                this.isWrongRequest = !data;
                if (!this.isWrongRequest) {
                    window.location.href = "/UserProjectsPage";
                }
            },
            (error) => this.isWrongRequest = true
        );
    }
}