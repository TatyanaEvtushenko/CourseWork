import { Component, EventEmitter, ViewChild } from '@angular/core';
import {Title} from '@angular/platform-browser';
import { MessageSenderService } from "../../services/messagesender.service";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { ProjectService } from '../../services/project.service';

import { MaterializeAction } from "angular2-materialize"

@Component({
    selector: 'homepage',
    templateUrl: './homepage.component.html'
})
export class HomePageComponent {
    lastNews: any;
    bigPayments: any;
    financedProjects: any;
    lastCreatedProjects: any;
    selectedProjectId: any;
    @ViewChild('carousel') carouselElement: any;
    actions = new EventEmitter<string>();

    constructor(private title: Title, protected messageSenderService: MessageSenderService,
        private messageSubscriberService: MessageSubscriberService, private projectService: ProjectService) {
        title.setTitle("Home page");
        window.setTimeout(() => {
            this.carouselElement.nativeElement.classList.toggle("initialized")
            this.actions.emit("carousel");
        }, 1000);
    }

    ngOnInit() {
        this.getLastNews();
        this.getBigPayments();
        this.getFinancedProjects();
        this.getLastCreatedProjects();
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

    private getLastCreatedProjects() {
        this.projectService.getLastCreatedProjects().subscribe(
            data => this.lastCreatedProjects = data
        );
    }

    private getFinancedProjects() {
        this.projectService.getFinancedProjects().subscribe(
            data => this.financedProjects = data
        );
    }

    private getBigPayments() {
        this.projectService.getBigPayments().subscribe(
            data => this.bigPayments = data
        );
    }

    private getLastNews() {
        this.projectService.getLastNews().subscribe(
            data => this.lastNews = data
        );
    }
}
