import { Component, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { SortingHelper } from '../../helpers/sorting.helper';
import { TimeHelper } from '../../helpers/time.helper';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { LocalizationService } from "../../services/localization.service";
declare var $: any;

@Component({
    selector: 'projecteditor',
    templateUrl: './projecteditor.component.html',
})

export class ProjectEditorComponent {
    @Input() projectForm = new NewProjectForm();
    @Input() isNewProject = true;
    isWorking = false;

    sortingHelper = new SortingHelper();
    timeHelper = new TimeHelper(this.localizationService);
    isWrongRequest = false;
    keys = ["NAME", "FUNDRAISINGEND", "DESCR", "IMAGE", "MINPAYMENT", "MAXPAYMENT", "FINANCIALPURPOSES", "CREATE", "INVALIDDATA",
        "EDITPROJECT", "EDIT", "TOPROJECTPAGE", 'ONE', 'TWO', 'THREE', 'FOUR', 'FIVE', "PATH", "CREATENEWPROJECT", "PROJECTCARD",
        "MAINCOLOR", "DONATES", "ACCOUNTNUMBER"];
    translations = {}

    constructor(public storage: StorageService,
        private title: Title,
        private projectService: ProjectService,
        private route: ActivatedRoute,
        private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
            this.initialize(title);
        });
    }

    ngOnInit() {
        if (!this.isNewProject) {
            const request = this.route.paramMap.switchMap((params: ParamMap) =>
                this.projectService.getProjectEditorForm(params.get('id')));
            request.subscribe(data => this.initializeUpdatedProject(data));
        } else {
            this.initializeNewProject();
        }
    }

    isValidTime() {
        return this.projectForm.fundRaisingEnd >= this.timeHelper.getNowTime();
    }

    addFinancialPurpose(purpose: any) {
        this.projectForm.financialPurposes.push(purpose);
        this.projectForm.financialPurposes.sort(this.sortingHelper.sortByBudget);
    }

    onSubmit() {
        this.isWorking = true;
        if (this.isNewProject) {
            this.projectService.addProject(this.projectForm).subscribe(
                (data) => this.getResponse(data, "/UserProjectsPage"),
                (error) => this.isWrongRequest = true
            );
        } else {
            this.projectService.updateProject(this.projectForm).subscribe(
                (data) => this.getResponse(data, `/ProjectPage/${this.projectForm.id}`),
                (error) => this.isWrongRequest = true
            );
        }
    }

    private getResponse(data: any, path: string) {
        this.isWrongRequest = !data;
        if (!this.isWrongRequest) {
            window.location.href = path;
        }
        this.isWorking = false;
    }

    private initializeNewProject() {
        this.projectForm.financialPurposes = [];
        this.projectForm.tags = [];
    }

    private initializeUpdatedProject(data: any) {
        data.financialPurposes.sort(this.sortingHelper.sortByBudget);
        this.projectForm = data;
        this.projectForm.imageBase64 = data.imageUrl;
    }

    private initialize(title: Title) {
        if (this.isNewProject) {
            title.setTitle(this.translations['CREATE']);
        } else {
            title.setTitle(this.translations['EDITPROJECT']);
        }
    }
}