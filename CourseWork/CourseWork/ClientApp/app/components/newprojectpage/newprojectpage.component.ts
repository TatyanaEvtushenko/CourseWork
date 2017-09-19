import { Component } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { SortingService } from '../../services/sorting.service';
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
declare var $: any;

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent{
    projectForm = new NewProjectForm();
    isWrongRequest = false;

    constructor(public storage: StorageService,
                private sortingService: SortingService,
    constructor(public storage: MessageSubscriberService,
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
        this.projectForm.financialPurposes.sort(this.sortingService.sortByBudget);
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