import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service'

@Component({
    selector: 'usersubscriptions',
    templateUrl: './usersubscriptions.component.html'
})
export class UserSubscriptionsComponent {
    projects: any[] = [];

    constructor(protected currentUserService: CurrentUserService,
        protected accountService: AccountService, private projectService: ProjectService, private storage: StorageService) {
    }

    ngOnInit() {
        this.projectService.getUserSubscribedProjects().subscribe((data) => {
            this.projects = data;
        });
    }
}