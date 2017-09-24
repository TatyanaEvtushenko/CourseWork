export class TimeHelper {

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
        const monthes = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        const month = monthes[date.getMonth()];
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