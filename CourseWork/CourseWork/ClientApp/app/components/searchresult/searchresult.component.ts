import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { StorageService } from "../../services/storage.service";
import { MessageSenderService } from "../../services/messagesender.service";
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, Router } from "@angular/router";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'searchresult',
    templateUrl: './searchresult.component.html'
})
export class SearchResultComponent {
    projects: any[] = [];
    selectedProjectId: string = null;
    keys = ["SEARCHRESULTS"];
    translations = {}

    constructor(private title: Title, private route: ActivatedRoute, private router: Router,
        protected accountService: AccountService, protected messageSenderService: MessageSenderService, private projectService: ProjectService,
        private storage: MessageSubscriberService, private localizationService: LocalizationService) {
        title.setTitle("Search results");
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            this.projectService.search(params['searchQuery']).subscribe(result => {
                this.projects = result;
            });
        });
    }

    openPayment(event: any) {
        this.selectedProjectId = event;
    }

    openNewsModal(event: any) {
        this.selectedProjectId = event;
    }

    subscribe(projectId: string) {
        this.projectService.subscribe(projectId).subscribe();
    }

    unsubscribe(projectId: string) {
        this.projectService.unsubscribe(projectId).subscribe();
    }
}