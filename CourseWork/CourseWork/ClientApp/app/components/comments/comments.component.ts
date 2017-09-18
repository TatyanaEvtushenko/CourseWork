import { Component, Input } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { TimeService } from '../../services/time.service';
import { SortingService } from '../../services/sorting.service';
import { StorageService } from '../../services/storage.service';

@Component({
    selector: 'comments',
    templateUrl: './comments.component.html',
})

export class CommentsComponent {
    @Input() comments: any;
    @Input() projectId: string;
    @Input() projectOwnerUserName: string;
    commentText = "";

    constructor(private projectService: ProjectService,
        public timeService: TimeService,
        private sortingService: SortingService,
        public storage: StorageService) {
    }

    ngOnInit() {
        this.comments.sort(this.sortingService.sortByTime);
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
        data.user = { userName: this.storage.currentUser.userName }
        this.comments.push(data);
    }
}