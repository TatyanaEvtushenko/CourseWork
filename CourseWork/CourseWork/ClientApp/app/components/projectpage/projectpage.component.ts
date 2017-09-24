import { Component} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { TimeHelper } from '../../helpers/time.helper';

@Component({
    selector: 'projectpage',
    templateUrl: './projectpage.component.html'
})
export class ProjectPageComponent {
    project: any = null;
    timeHelper = new TimeHelper();

    constructor(public storage: MessageSubscriberService,
        private route: ActivatedRoute,
        private title: Title,
        private projectService: ProjectService) {
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
        news.time = this.timeHelper.getNowTime();
        this.project.news = [news].concat(this.project.news);
    }

    updateRating() {
        this.projectService.changeRating(this.project.id, this.project.rating).subscribe();
    }

    getNeccessaryAmount() {
        const budgets = this.project.financialPurposes.map((x : any)=> x.budget);
        return Math.max.apply(null, budgets);
    }

    subscribe() {
        this.projectService.subscribe(this.project.id).subscribe(
            data => this.project.isSubscriber = data,
            error => this.project.isSubscriber = false
        );
        this.project.isSubscriber = true;
    }

    unsubscribe() {
        this.projectService.unsubscribe(this.project.id).subscribe(
            data => this.project.isSubscriber = !data,
            error => this.project.isSubscriber = true
        );
        this.project.isSubscriber = false;
    }
}