import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { StorageService } from '../../services/storage.service';
import { SortingService } from '../../services/sorting.service';

@Component({
    selector: 'projectpage',
    templateUrl: './projectpage.component.html'
})
export class ProjectPageComponent {
    project: any = null;

    constructor(public storage: StorageService,
        private route: ActivatedRoute,
        private title: Title,
        private projectService: ProjectService,
        private sortingService: SortingService) {
    }

    ngOnInit() {
        const request = this.route.paramMap.switchMap((params: ParamMap) =>
            this.projectService.getProject(params.get('id')));
        request.subscribe(data => {
            this.prepareData(data);
            this.project = data;
            this.title.setTitle(data.name);
        });
    }

    updateRating() {
        this.projectService.changeRating(this.project.id, this.project.rating).subscribe();
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

    deleteFinancialPurpose(purpose: any) {
        alert("delete");
    }

    private prepareData(data: any) {
        data.financialPurposes.sort(this.sortingService.sortByBudget);
        data.news.sort(this.sortingService.sortByTime);
        data.comments.sort(this.sortingService.sortByTime);
        console.log(data);
    }
}