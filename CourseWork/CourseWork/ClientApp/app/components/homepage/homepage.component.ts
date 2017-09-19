import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';
import { MessageSenderService } from "../../services/messagesender.service";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { ProjectService } from '../../services/project.service';

@Component({
    selector: 'homepage',
    templateUrl: './homepage.component.html'
})
export class HomePageComponent {
    lastNews: any;
    bigPayments: any;

    constructor(private title: Title, protected messageSenderService: MessageSenderService,
        private messageSubscriberService: MessageSubscriberService, private projectService: ProjectService) {
        title.setTitle("Home page");
    }

    ngOnInit() {
        this.getLastNews();
        this.getBigPayments();
    }

    private getBigPayments() {
        this.projectService.getBigPayments().subscribe(
            data => {
                this.bigPayments = data;
                console.log(this.bigPayments);
            }
        );
    }

    private getLastNews() {
        this.projectService.getLastNews().subscribe(
            data => {
                this.lastNews = data;
                console.log(this.lastNews);
            }
        );
    }
}
