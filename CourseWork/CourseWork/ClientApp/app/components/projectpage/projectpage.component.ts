import { Component} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { TimeService } from '../../services/time.service';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'projectpage',
    templateUrl: './projectpage.component.html'
})
export class ProjectPageComponent {
    project: any = null;
    keys = ["ONE", "TWO", "THREE", "FOUR", "FIVE", "DESCR", "FINANCIALPURPOSES", "COMMENTS", "NEWS",
        "EDITPROJECT", "EDIT", "TOPROJECTPAGE", "SUBSCRIBE", "UNSUBSCRIBE", "PAYEDAMOUNT", "ENDDATE", "PROJECTSTATUS", "PAYMENTS"];
    translations = {}

    constructor(public storage: MessageSubscriberService,
        public timeService: TimeService,
        private route: ActivatedRoute,
        private title: Title,
        private projectService: ProjectService,
        private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    ngOnInit() {
        const request = this.route.paramMap.switchMap((params: ParamMap) =>
            this.projectService.getProject(params.get('id')));
        request.subscribe(data => {
            this.project = data;
            this.title.setTitle(data.name);
        });
    }

    addNews(news: any) {
        news.time = this.timeService.getNowTime();
        this.project.news = [news].concat(this.project.news);
    }

    updateRating() {
        this.projectService.changeRating(this.project.id, this.project.rating).subscribe();
    }

    subscribe() {
        this.projectService.subscribe(this.project.id).subscribe(
            data => {
                this.project.isSubscriber = true;
            },
            error => this.project.isSubscriber = false
        );
        
    }

    unsubscribe() {
        this.projectService.unsubscribe(this.project.id).subscribe(
            data => {
                this.project.isSubscriber = false;
            },
            error => this.project.isSubscriber = true
        );
    }
}