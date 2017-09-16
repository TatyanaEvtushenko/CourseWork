import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { MessageSubscriber } from '../message.subscriber';
import { ProjectService } from '../../services/project.service';
import { MessageSenderService } from '../../services/messagesender.service';

@Component({
    selector: 'userpage',
    templateUrl: './userpage.component.html'
})
export class UserPageComponent extends MessageSubscriber {
    projects: any[] = [];
    displayableInfo: any;

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, protected messageSenderService: MessageSenderService, private projectService: ProjectService) {
        super(currentUserService, accountService, messageSenderService);
        title.setTitle("My page");
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
        this.accountService.getCurrentUserDisplayableInfo().subscribe((data) => {
            this.displayableInfo = data;
            console.log(this.displayableInfo);
        });
    }
}