import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
//import { MessageSubscriber } from '../message.subscriber';
import { ProjectService } from '../../services/project.service';
import { MessageSenderService } from '../../services/messagesender.service';
import { DisplayableInfo } from "../../viewmodels/displayableinfo";
import { AccountEditForm } from "../../viewmodels/accounteditform";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';

@Component({
    selector: 'userpage',
    templateUrl: './userpage.component.html'
})
export class UserPageComponent {
    projects: any[] = [];
    displayableInfo: DisplayableInfo = { userName: "", about: "", projectNumber: 0, avatar: "", contacts: "", registrationTime: "" };
    accountEditForm: AccountEditForm = { about: "about", contacts: "contacts" };

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService,
        protected messageSenderService: MessageSenderService, private projectService: ProjectService, private storage: MessageSubscriberService) {
        //super(currentUserService, accountService, messageSenderService);
        title.setTitle("My page");
    }

    ngOnInit() {
        //console.log(JSON.stringify(this.displayableInfo));
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
            this.displayableInfo.about = this.displayableInfo.about || "";
            this.displayableInfo.contacts = this.displayableInfo.contacts || "";
            this.accountEditForm = { about: this.displayableInfo.about, contacts: this.displayableInfo.contacts };
        });
    }
}