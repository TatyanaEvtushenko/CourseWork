import { Component, Input } from '@angular/core';
import { AwardType } from "../../enums/awardtype";
import { LocalizationService } from '../../services/localization.service';

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

    constructor(private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    getAwardImage() {
        switch (this.award.type) {
            case AwardType.ForComments:
                return "/images/award_blue.png";
            case AwardType.ForPayments:
                return "/images/award_green.png";
            case AwardType.ForProjects:
                return "/images/award_orange.png";
            case AwardType.ForReceivedPayments:
                return "/images/award_red.png";
            case AwardType.ForSubscriptions:
            default:
                return "/images/award_violet.png";
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