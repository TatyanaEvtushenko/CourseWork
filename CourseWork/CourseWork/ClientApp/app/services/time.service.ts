import { Injectable } from '@angular/core';

@Injectable()
export class TimeService {

    getFormatDate(notConvertDate: any) {
        const date = new Date(notConvertDate);
        const hours = this.toTwoDigits(date.getHours());
        const minutes = this.toTwoDigits(date.getMinutes());
        const seconds = this.toTwoDigits(date.getSeconds());
        return `${this.getFormatOnlyDate(notConvertDate)} ${hours}:${minutes}:${seconds}`;
    }

    getFormatOnlyDate(notConvertDate: any) {
        const date = new Date(notConvertDate);
        const month = this.toTwoDigits(date.getMonth() + 1);
        const day = this.toTwoDigits(date.getDay() + 1);
        return `${date.getFullYear()}-${month}-${day}`;
    }

    getNowTime() {
        return new Date(Date.now());
    }

    isNotPast(date: any) {
        return new Date(date) >= new Date(Date.now());
    }

    private toTwoDigits(number: any) {
        const strNumber = number.toString();
        if (strNumber.length <= 1) {
            return `0${strNumber}`;
        }
        return strNumber;
    }
}