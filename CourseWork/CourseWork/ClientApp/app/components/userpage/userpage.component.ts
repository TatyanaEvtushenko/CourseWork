﻿import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { ProjectService } from '../../services/project.service';
import { MessageSenderService } from '../../services/messagesender.service';
import { DisplayableInfo } from "../../viewmodels/displayableinfo";
import { AccountEditForm } from "../../viewmodels/accounteditform";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { SortingHelper } from '../../helpers/sorting.helper';

@Component({
    selector: 'userpage',
    templateUrl: './userpage.component.html'
})
export class UserPageComponent {
    ownerUserName: string = null;
    currentUserName: string = null;
    userProjects: any[] = [];
    userSubscribedProjects: any[] = [];
    displayableInfo: DisplayableInfo = null;
    accountEditForm: AccountEditForm = null;
    selectedProjectId: string = null;
    isInitialized = false;
    sortingHelper = new SortingHelper();

    constructor(private title: Title,
        protected currentUserService: CurrentUserService,
        protected accountService: AccountService,
        protected messageSenderService: MessageSenderService,
        private projectService: ProjectService,
        private storage: MessageSubscriberService,
        private route: ActivatedRoute) {
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
                data.sort(this.sortingHelper.sortByProjectStatus);
                this.userProjects = data;
            }
        );
    }

    private getDisplayableInfo() {
        this.accountService.getUserDisplayableInfo(this.ownerUserName).subscribe((data: DisplayableInfo) => {
            this.displayableInfo = data;
            this.displayableInfo.about = data.about || "";
            this.displayableInfo.contacts = data.contacts || "";
            this.accountEditForm = { about: this.displayableInfo.about, contacts: this.displayableInfo.contacts };
            this.isInitialized = true;
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