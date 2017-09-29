import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';
import { MessageSenderService } from "../../services/messagesender.service";
import { StorageService } from '../../services/storage.service';
import { ProjectService } from '../../services/project.service';
import { LocalizationService } from '../../services/localization.service';
declare var $: any;

@Component({
    selector: 'homepage',
    templateUrl: './homepage.component.html'
})
export class HomePageComponent {
    selectedProjectId: string = null;
    lastNews: any = null;
    bigPayments: any = null;
    financedProjects: any = null;
    lastCreatedProjects: any = null;
    keys = ["HOMEPAGE", "ABOUTUS", "STARTPROJECT", "LASTPROJECTS", "BIGGESTPAYMENTS", "SUCCESSFULPROJECTS", "LATESTNEWS"];
    translations = {}

    constructor(private title: Title,
        protected messageSenderService: MessageSenderService,
        private storage: StorageService,
        private projectService: ProjectService,
        private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
            title.setTitle(this.translations['HOMEPAGE']);
        });
    }

    ngOnInit() {
        this.getLastNews();
        this.getBigPayments();
        this.getFinancedProjects();
        this.getLastCreatedProjects();
    }

    private getLastCreatedProjects() {
        this.projectService.getLastCreatedProjects().subscribe(
            data => this.lastCreatedProjects = data
        );
    }

    private getFinancedProjects() {
        this.projectService.getFinancedProjects().subscribe(
            data => this.financedProjects = data
        );
    }

    private getBigPayments() {
        this.projectService.getBigPayments().subscribe(
            data =>  this.bigPayments = data
        );
    }

    private getLastNews() {
        this.projectService.getLastNews().subscribe(
            data => this.lastNews = data
        );
    }
}
