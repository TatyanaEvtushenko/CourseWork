﻿import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { MessageSubscriber } from '../message.subscriber';
import { MessageSenderService } from "../../services/messagesender.service";
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'searchresult',
    templateUrl: './searchresult.component.html'
})
export class SearchResultComponent extends MessageSubscriber {
    projects: any[] = [];

    constructor(private title: Title, private route: ActivatedRoute, private router: Router,
      protected currentUserService: CurrentUserService, protected accountService: AccountService,
      protected messageSenderService: MessageSenderService, private projectService: ProjectService) {
        super(currentUserService, accountService, messageSenderService);
        title.setTitle("My projects");
    }

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            this.projectService.search(params['searchQuery']).subscribe(result => {
                this.projects = result;
            });
        });
    }
}