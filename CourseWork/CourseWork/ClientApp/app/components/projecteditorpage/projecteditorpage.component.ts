import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { SortingHelper } from '../../helpers/sorting.helper';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { LocalizationService } from "../../services/localization.service";
declare var $: any;

@Component({
    selector: 'projecteditorpage',
    templateUrl: './projecteditorpage.component.html',
})

export class ProjectEditorPageComponent {
    project: NewProjectForm = null;
    isWrongRequest = false;
    keys = ["NAME", "FUNDRAISINGEND", "DESCR", "IMAGE", "MINPAYMENT", "MAXPAYMENT", "FINANCIALPURPOSES", "CREATE", "INVALIDDATA",
        "EDITPROJECT", "EDIT", "TOPROJECTPAGE"];
    translations = {}
    sortingHelper = new SortingHelper();

    constructor(public storage: StorageService,
        private title: Title,
        private projectService: ProjectService,
        private route: ActivatedRoute,
        private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
            title.setTitle(this.translations['EDITPROJECT']);
        });
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
        this.project.financialPurposes.sort(this.sortingHelper.sortByBudget);
    }

    onSubmit() {
        this.projectService.updateProject(this.project).subscribe(
            (data) => {
                this.isWrongRequest = !data;
                if (!this.isWrongRequest) {
                    window.location.href = `/ProjectPage/${this.project.id}`;
                }
            },
            (error) => this.isWrongRequest = true
        );
    }

    private prepareData(data: any) {
        data.financialPurposes.sort(this.sortingHelper.sortByBudget);
        this.project = data;
        this.project.imageBase64 = data.imageUrl;
    }
}