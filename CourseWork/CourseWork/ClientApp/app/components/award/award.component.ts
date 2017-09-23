import { Component, Input } from '@angular/core';
import { AwardType } from "../../enums/awardtype";

@Component({
    selector: 'award',
    templateUrl: './award.component.html',
})

export class AwardComponent {
    @Input() award: any;
    @Input() isSmall = false;

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
                return "Писатель";
            case AwardType.ForPayments:
                return "Инвестор";
            case AwardType.ForProjects:
                return "Создатель";
            case AwardType.ForReceivedPayments:
                return "Бизнесмен";
            case AwardType.ForSubscriptions:
            default:
                return "Душа компании";
        }
    }

    getDescription() {
        switch (this.award.type) {
            case AwardType.ForComments:
                return `Создал ${this.award.neccessaryCount} комментариев`;
            case AwardType.ForPayments:
                return `Совершил оплату в $${this.award.neccessaryCount}`;
            case AwardType.ForProjects:
                return `Создал ${this.award.neccessaryCount} проектов`;
            case AwardType.ForReceivedPayments:
                return `$${this.award.neccessaryCount} было перечислено на проекты пользователя`;
            case AwardType.ForSubscriptions:
            default:
                return `${this.award.neccessaryCount} человек подписались на проекты`;
        }
    }
}