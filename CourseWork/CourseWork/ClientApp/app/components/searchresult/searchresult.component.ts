import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { StorageService } from "../../services/storage.service";
import { MessageSenderService } from "../../services/messagesender.service";
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, Router } from "@angular/router";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';

@Component({
    selector: 'searchresult',
    templateUrl: './searchresult.component.html'
})
export class SearchResultComponent {
    projects: any[] = [];

    constructor(private title: Title, private route: ActivatedRoute, private router: Router,
      protected accountService: AccountService, protected messageSenderService: MessageSenderService, private projectService: ProjectService, private storage: MessageSubscriberService) {
        title.setTitle("Search results");
    }

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            this.projectService.search(params['searchQuery']).subscribe(result => {
                this.projects = result;
            });
        });
    }
}