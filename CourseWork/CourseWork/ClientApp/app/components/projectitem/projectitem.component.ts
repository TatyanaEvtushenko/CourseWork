﻿import { Component, Input, Output, EventEmitter } from '@angular/core';
import { StorageService } from '../../services/storage.service';
import { LocalizationService } from "../../services/localization.service";
import { TimeHelper } from '../../helpers/time.helper';

@Component({
    selector: 'projectitem',
    templateUrl: './projectitem.component.html',
})

export class ProjectItemComponent {
    @Input() project: any;
    @Output() onClickNews = new EventEmitter<string>();
    @Output() onClickSubscribe = new EventEmitter<string>();
    @Output() onClickUnsubscribe = new EventEmitter<string>();
    @Output() onClickPay = new EventEmitter<string>();
    timeHelper = new TimeHelper(this.localizationService);
    keys = ["ABOUT", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SUBSCRIBE", "UNSUBSCRIBE", "PAY", "EDIT", "NEWS"];
    translations = {}

    constructor(public storage: StorageService,
        public localizationService: LocalizationService) {
            this.localizationService.getTranslations(this.keys).subscribe((data) => {
                this.translations = data;
            });
    }

    openNews() {
        this.onClickNews.emit(this.project.id);
    }

    openPayment() {
        this.onClickPay.emit(this.project.id);
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