import { Component, Input, DoCheck, ChangeDetectorRef } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { TimeHelper } from '../../helpers/time.helper';
import { SortingHelper } from '../../helpers/sorting.helper';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'news',
    templateUrl: './news.component.html',
})
export class NewsComponent implements DoCheck {
    @Input() canChange = false;
    @Input() viewProjectName = false;
    @Input() someNews: any;
    sortingHelper = new SortingHelper();
    timeHelper = new TimeHelper(this.localizationService);
    isSorted = false;
    keys = ["FROMPROJECT"];
    translations = {};

    constructor(private projectService: ProjectService, private cdr: ChangeDetectorRef, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe(data => {
            this.translations = data;
        });
    }

    ngDoCheck() {
        if (this.someNews != null && !this.isSorted) {
            this.someNews.sort(this.sortingHelper.sortByTimeDescending);
            this.isSorted = true;
            this.cdr.detectChanges();
        }
    }

    delete(news: any) {
        const index = this.someNews.indexOf(news);
        if (index >= 0) {
            this.someNews.splice(index, 1);
        }
        this.projectService.deleteNews(news.id).subscribe();
    }
}