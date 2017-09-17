import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { ProjectService } from '../../services/project.service';
import { MessageSenderService } from '../../services/messagesender.service';
import { DisplayableInfo } from "../../viewmodels/displayableinfo";
import { AccountEditForm } from "../../viewmodels/accounteditform";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { SortingService } from '../../services/sorting.service';

@Component({
    selector: 'userpage',
    templateUrl: './userpage.component.html'
})
export class UserPageComponent {
    userProjects: any[] = [];
    userSubscribedProjects: any[] = []
    displayableInfo: DisplayableInfo = { userName: "", about: "", projectNumber: 0, avatar: "", contacts: "", registrationTime: "" };
    accountEditForm: AccountEditForm = { about: "about", contacts: "contacts" };
    selectedProjectId: string = null;

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService,
        protected messageSenderService: MessageSenderService, private projectService: ProjectService, private storage: MessageSubscriberService,
        private sortingService: SortingService) {
        title.setTitle("My page");
    }

    ngOnInit() {
        this.projectService.getUserProjects().subscribe(
            (data) => {
                data.sort(this.sortingService.sortByProjectStatus);
                this.userProjects = data;
            }
        );
        this.accountService.getCurrentUserDisplayableInfo().subscribe((data) => {
            this.displayableInfo = data;
            this.displayableInfo.about = this.displayableInfo.about || "";
            this.displayableInfo.contacts = this.displayableInfo.contacts || "";
            this.accountEditForm = { about: this.displayableInfo.about, contacts: this.displayableInfo.contacts };
        });
        this.projectService.getUserSubscribedProjects().subscribe((data) => {
            this.userSubscribedProjects = data;
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