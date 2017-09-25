import { LocalizationService } from "../services/localization.service";

export class TimeHelper {
    keys = ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];
    translations = {};

    constructor(private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    getFormatDate(notConvertDate: any) {
        const date = new Date(notConvertDate);
        return `${this.convertDateToDate(date)} ${this.convertDateToTime(date)}`;
    }

    getFormatOnlyDate(notConvertDate: any) {
        const date = new Date(notConvertDate);
        return this.convertDateToDate(date);
    }

    getNowTime() {
        return new Date(Date.now());
    }

    isNotPast(date: any) {
        return new Date(date) >= new Date(Date.now());
    }

    private convertDateToDate(date: Date) {
        const month = this.translations[this.keys[date.getMonth()]];
        const day = date.getDate();
        return `${day} ${month} ${date.getFullYear()}`;
    }

    private convertDateToTime(date: Date) {
        const hours = this.toTwoDigits(date.getHours());
        const minutes = this.toTwoDigits(date.getMinutes());
        return `${hours}:${minutes}`;
    }

    private toTwoDigits(number: any) {
        const strNumber = number.toString();
        if (strNumber.length <= 1) {
            return `0${strNumber}`;
        }
        return strNumber;
    }
}