import { Component, Input } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { TimeService } from '../../services/time.service';
import { SortingService } from '../../services/sorting.service';

@Component({
    selector: 'news',
    templateUrl: './news.component.html',
})

export class NewsComponent {
    @Input() canChange = false;
    @Input() someNews: any;

    constructor(private projectService: ProjectService,
        public timeService: TimeService,
        private sortingService: SortingService) {
    }

    ngOnInit() {
        this.someNews.sort(this.sortingService.sortByTime);
    }

    delete(news: any) {
        const index = this.someNews.indexOf(news);
        if (index >= 0) {
            this.someNews.splice(index, 1);
        }
        this.projectService.deleteNews(news.id).subscribe();
    }
}