import { Component, Input } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { TimeService } from '../../services/time.service';

@Component({
    selector: 'news',
    templateUrl: './news.component.html',
})

export class NewsComponent {
    @Input() canChange = false;
    @Input() someNews: any;

    constructor(private projectService: ProjectService, public timeService: TimeService){}

    delete(news: any) {
        this.projectService.deleteNews(news.id).subscribe(
            data => this.getResponse(data, news)
        );
    }

    private getResponse(response: boolean, news: any) {
        if (response) {
            const index = this.someNews.indexOf(news);
            if (index >= 0) {
                this.someNews.splice(index, 1);
            }
        }
    }
}