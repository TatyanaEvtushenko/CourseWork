import { Component, EventEmitter, ViewChild } from '@angular/core';
import {Title} from '@angular/platform-browser';
import { MessageSenderService } from "../../services/messagesender.service";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { ProjectService } from '../../services/project.service';
declare var $: any;

import { MaterializeAction } from "angular2-materialize"

@Component({
    selector: 'homepage',
    templateUrl: './homepage.component.html'
})
export class HomePageComponent {
    lastNews: any = null;
    bigPayments: any = null;
    financedProjects: any = null;
    lastCreatedProjects: any = null;
    selectedProjectId: any = null;

    constructor(private title: Title, protected messageSenderService: MessageSenderService,
        private messageSubscriberService: MessageSubscriberService, private projectService: ProjectService) {
        title.setTitle("Home page");
        // example of a hacky way to add an image to the carousel dynamically
        window.setTimeout(() => {
           // this.imageURLs = [this.imageURLs[0], ...this.imageURLs]; // duplicate the first iamge
            this.carouselElement.nativeElement.classList.toggle("initialized");
            this.actions.emit("carousel");
        }, 1000);
    }

    @ViewChild('carousel') carouselElement;
    actions = new EventEmitter<string>();

    imageURLs = [
        "https://image.shutterstock.com/display_pic_with_logo/1264645/364153082/stock-photo-asian-girl-in-sunflower-field-364153082.jpg",
        "https://image.shutterstock.com/display_pic_with_logo/1264645/298927574/stock-photo-a-young-traveler-girl-sit-on-the-wooden-bridge-in-halong-bay-and-enjoy-the-beauty-of-seascape-298927574.jpg",
        "https://image.shutterstock.com/display_pic_with_logo/1264645/298757792/stock-photo-a-young-traveler-girl-sit-on-the-top-of-mountain-in-halong-bay-and-enjoy-the-beauty-of-seascape-298757792.jpg",
        "https://image.shutterstock.com/display_pic_with_logo/2565601/411902890/stock-photo-ha-long-bay-scenic-view-hanoi-vietnam-411902890.jpg",
        "https://image.shutterstock.com/display_pic_with_logo/2565601/413207668/stock-photo-the-temple-of-literature-in-hanoi-vietnam-the-chinese-words-is-poem-of-thie-temple-and-templs-s-413207668.jpg"
    ];

    showInitialized = false;

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
