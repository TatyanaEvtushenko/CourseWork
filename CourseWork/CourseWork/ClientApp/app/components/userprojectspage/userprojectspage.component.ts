import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
//import { MessageSubscriber } from '../message.subscriber';
import { MessageSenderService } from "../../services/messagesender.service";
import { ProjectService } from '../../services/project.service';
import { StorageService } from '../../services/storage.service';
import { SortingService } from '../../services/sorting.service';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent {
    projects: any[] = [];
    selectedProjectId: string = null;

    constructor(public storage: StorageService,
        private title: Title,
        private sortingService: SortingService,
        private projectService: ProjectService) {
        title.setTitle("My projects");
    }

    ngOnInit() {
        this.projectService.getUserProjects().subscribe(
            (data) => {
                data.sort(this.sortingService.sortByProjectStatus);
                this.projects = data;
            }
        );
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