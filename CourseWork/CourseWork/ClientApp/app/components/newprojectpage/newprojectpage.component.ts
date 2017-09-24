import { Component } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { SortingHelper } from '../../helpers/sorting.helper';
import { TimeHelper } from '../../helpers/time.helper';
import { ColorPickerService } from 'ngx-color-picker';
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
    sortingHelper = new SortingHelper();
    timeHelper = new TimeHelper(this.localizationService);
    keys = ["NAME", "FUNDRAISINGEND", "DESCR", "IMAGE", "MINPAYMENT", "MAXPAYMENT", "FINANCIALPURPOSES", "CREATE", "PROJECTERROR",
        "CREATENEWPROJECT"];
    translations = {}

    constructor(public storage: MessageSubscriberService,
        private title: Title,
        private projectService: ProjectService,
        private cpService: ColorPickerService,
        private localizationService: LocalizationService) {
        title.setTitle("New project");
        this.initializeProject();
        this.localizationService.getTranslations(this.keys).subscribe((data: any) => {
            this.translations = data;
        });
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