import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { MessageSubscriber } from '../message.subscriber';
import { MessageSenderService } from "../../services/messagesender.service";
import { ProjectService } from '../../services/project.service';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent extends MessageSubscriber {
    projects: any[] = [];
    selectedProjectId: string = null;

	constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, protected messageSenderService: MessageSenderService, private projectService: ProjectService) {
        super(currentUserService, accountService, messageSenderService);
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