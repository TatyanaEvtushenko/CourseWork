import { Component, Input, Output, EventEmitter } from '@angular/core';
import { StorageService } from '../../services/storage.service';
import { TimeService } from '../../services/time.service';

@Component({
    selector: 'projectitem',
    templateUrl: './projectitem.component.html',
})

export class ProjectItemComponent {
    @Input() project: any;
    @Output() onClickNews = new EventEmitter<string>();
    @Output() onClickSubscribe = new EventEmitter<string>();
    @Output() onClickUnsubscribe = new EventEmitter<string>();

    constructor(public storage: StorageService, public timeService: TimeService) { }

    openNews() {
        this.onClickNews.emit(this.project.id);
    }

    subscribe() {
        this.onClickSubscribe.emit(this.project.id);
        this.project.isSubscriber = true;
    }

    unsubscribe() {
        this.onClickUnsubscribe.emit(this.project.id);
        this.project.isSubscriber = false;
    }
}