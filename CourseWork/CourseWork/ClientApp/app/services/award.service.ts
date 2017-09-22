import { Injectable } from '@angular/core';
import { AwardType } from "../enums/awardtype";

@Injectable()
export class AwardService {
    awardType = AwardType;

    getAwardImage(awardType: AwardType) {
        switch (awardType) {
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

    getName(awardType: AwardType) {
        switch (awardType) {
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

    getDescription(award: any) {
        switch (award.type) {
            case AwardType.ForComments:
                return `Создал ${award.neccessaryCount} комментариев`;
            case AwardType.ForPayments:
                return `Совершил оплату в $${award.neccessaryCount}`;
            case AwardType.ForProjects:
                return `Создал ${award.neccessaryCount} проектов`;
            case AwardType.ForReceivedPayments:
                return `$${award.neccessaryCount} было перечислено на проекты пользователя`;
            case AwardType.ForSubscriptions:
            default:
                return `${award.neccessaryCount} человек подписались на проекты`;
        }
    }
}