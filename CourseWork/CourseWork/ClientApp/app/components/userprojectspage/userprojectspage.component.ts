import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent extends CurrentUserSubscriber {
    projects: any[] = [];
    selectedProjectId: string = null;

    constructor(private title: Title,
        private projectService: ProjectService,
        protected currentUserService: CurrentUserService,
        protected accountService: AccountService) {
        super(currentUserService, accountService);
        title.setTitle("My projects");
    }

    ngOnInit() {
        this.projectService.getUserProjects().subscribe(
            (data) => {
                this.projects = data;
                this.projects.sort((a, b) => {
                    if (a.status > b.status) {
                        return 1;
                    }
                    if (a.status === b.status) {
                        return 0;
                    } else {
                        return -1;
                    }
                });
            }
        );
    }

    openNewsModal(event: any) {
        this.selectedProjectId = event;
    }
}