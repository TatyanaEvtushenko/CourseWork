import { Component } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { SortingService } from '../../services/sorting.service';
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { LocalizationService } from "../../services/localization.service";
declare var $: any;

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent{
    projectForm = new NewProjectForm();
    isWrongRequest = false;
    keys = ["NAME", "FUNDRAISINGEND", "DESCR", "IMAGE", "MINPAYMENT", "MAXPAYMENT", "FINANCIALPURPOSES", "CREATE", "PROJECTERROR",
        "CREATENEWPROJECT"];
    translations = {}

    constructor(public storage: MessageSubscriberService,
                private title: Title, 
                private projectService: ProjectService,
                private sortingService: SortingService,
                private localizationService: LocalizationService) {
        title.setTitle("New project");
        this.projectForm.financialPurposes = [];
        this.projectForm.tags = [];
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
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