﻿import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, ParamMap } from '@angular/router';
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
    ownerUserName: string = null;
    currentUserName: string = null;
    userProjects: any[] = [];
    userSubscribedProjects: any[] = [];
    displayableInfo: DisplayableInfo = { userName: "", about: "", projectNumber: 0, avatar: "", contacts: "", registrationTime: "" };
    accountEditForm: AccountEditForm = { about: "about", contacts: "contacts" };
    selectedProjectId: string = null;

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService,
        protected messageSenderService: MessageSenderService, private projectService: ProjectService, private storage: MessageSubscriberService,
        private sortingService: SortingService, private route: ActivatedRoute) {
        title.setTitle("My page");
    }

    ngOnInit() {
        this.subscribeToPageOwner();
    }

    private subscribeToPageOwner() {
        this.storage.isReady.subscribe((data: void) => {
            this.currentUserName = this.storage.currentUser.userName;
            this.route.queryParams.subscribe((params) => {
                this.ownerUserName = params['username'] || this.currentUserName;
                this.getInfo();
            });
        });
    }

    private getInfo() {
        this.getUserProjects();
        this.getDisplayableInfo();
        this.getUserSubscribedProjects();
    }

    private getUserProjects() {
        this.projectService.getProjects(this.ownerUserName).subscribe(
            (data) => {
                data.sort(this.sortingService.sortByProjectStatus);
                this.userProjects = data;
            }
        );
    }

    private getDisplayableInfo() {
        this.accountService.getUserDisplayableInfo(this.ownerUserName).subscribe((data) => {
            this.displayableInfo = data;
            this.displayableInfo.about = this.displayableInfo.about || "";
            this.displayableInfo.contacts = this.displayableInfo.contacts || "";
            this.accountEditForm = { about: this.displayableInfo.about, contacts: this.displayableInfo.contacts };
        });
    }

    private getUserSubscribedProjects() {
        this.projectService.getSubscribedProjects(this.ownerUserName).subscribe((data) => {
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

    updateAccount(editForm: AccountEditForm) {
        this.displayableInfo.about = editForm.about;
        this.displayableInfo.contacts = editForm.contacts;
    }

    updateAvatar(newAvatar: string) {
        this.displayableInfo.avatar = newAvatar;
    }
}