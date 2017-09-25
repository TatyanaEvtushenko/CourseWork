import { Component, Input, AfterViewInit } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { TimeHelper } from '../../helpers/time.helper';
import { SortingHelper } from '../../helpers/sorting.helper';
import { StorageService } from '../../services/storage.service';
import { LocalizationService } from '../../services/localization.service';

@Component({
    selector: 'comments',
    templateUrl: './comments.component.html',
})

export class CommentsComponent implements AfterViewInit {

    @Input() comments: any;
    @Input() projectId: string;
    @Input() projectOwnerUserName: string;
    commentText = "";
    timeHelper = new TimeHelper(this.localizationService);
    sortingHelper = new SortingHelper();
    keys = ["NEWCOMMENT"];
    translations = {}

    constructor(private projectService: ProjectService,
        public storage: StorageService, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    ngAfterViewInit() {
        this.comments.sort(this.sortingHelper.sortByTimeDescending);
        this.comments.reverse();
    }

    add() {
        if (this.commentText != null && this.commentText !== "") {
            this.projectService.addComment(this.projectId, this.commentText).subscribe(
                data => this.addComments(data)
            );
        }
    }

    delete(comment: any) {
        const index = this.comments.indexOf(comment);
        if (index >= 0) {
            this.comments.splice(index, 1);
        }
        this.projectService.removeComment(comment.id).subscribe();
    }

    private addComments(data: any) {
        this.commentText = "";
        this.comments.push(data);
    }
}