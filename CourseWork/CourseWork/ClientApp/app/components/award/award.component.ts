import { Component, Input } from '@angular/core';
import { AwardType } from "../../enums/awardtype";
import { LocalizationService } from '../../services/localization.service';
import { AppConfig } from '../../app.config';

@Component({
    selector: 'award',
    templateUrl: './award.component.html',
})

export class AwardComponent {
    @Input() award: any;
    @Input() isSmall = false;
    keys = ["CREATOR", "WRITER", "INVESTOR", "BUSINESSMAN", "PEOPLEPERSON", "CREATED", "COMMENTS_A", "PAID",
        "PROJECTS_A", "WASPAID", "SUBSCRIBEDFOR"];
    translations = {}

    constructor(private localizationService: LocalizationService, private config: AppConfig) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    getAwardImage() {
        switch (this.award.type) {
            case AwardType.ForComments:
                return this.config.getConfig('awardImages')['blue'];
            case AwardType.ForPayments:
                return this.config.getConfig('awardImages')['green'];
            case AwardType.ForProjects:
                return this.config.getConfig('awardImages')['orange'];
            case AwardType.ForReceivedPayments:
                return this.config.getConfig('awardImages')['red'];
            case AwardType.ForSubscriptions:
            default:
                return this.config.getConfig('awardImages')['violet'];
        }
    }

    getName() {
        switch (this.award.type) {
            case AwardType.ForComments:
                return this.translations['WRITER'];
            case AwardType.ForPayments:
                return this.translations['INVESTOR'];
            case AwardType.ForProjects:
                return this.translations['CREATOR'];
            case AwardType.ForReceivedPayments:
                return this.translations['BUSINESSMAN'];
            case AwardType.ForSubscriptions:
            default:
                return this.translations['PEOPLEPERSON'];
        }
    }

    getDescription() {
        switch (this.award.type) {
            case AwardType.ForComments:
                return `${this.translations['CREATED']} ${this.award.neccessaryCount} ${this.translations['COMMENTS_A']}`;
            case AwardType.ForPayments:
                return `${this.translations['PAID']} ${this.award.neccessaryCount}`;
            case AwardType.ForProjects:
                return `${this.translations['CREATED']} ${this.award.neccessaryCount} ${this.translations['PROJECT_A']}`;
            case AwardType.ForReceivedPayments:
                return `$${this.award.neccessaryCount} ${this.translations['WASPAID']}`;
            case AwardType.ForSubscriptions:
            default:
                return `${this.award.neccessaryCount} ${this.translations['SUBSCRIBEDFOR']}`;
        }
    }
}