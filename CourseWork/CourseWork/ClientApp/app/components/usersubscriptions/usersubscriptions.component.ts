import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { ProjectService } from '../../services/project.service';

@Component({
    selector: 'usersubscriptions',
    templateUrl: './usersubscriptions.component.html'
})
export class UserSubscriptionsComponent extends CurrentUserSubscriber {
    projects: any[] = [];

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, private projectService: ProjectService) {
        super(currentUserService, accountService);
        title.setTitle("My projects");
    }

    ngOnInit() {
        this.projectService.getUserSubscribedProjects().subscribe((data) => {
            this.projects = data;
        });
    }
}