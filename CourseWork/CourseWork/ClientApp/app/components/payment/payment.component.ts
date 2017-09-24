import { Component, Input } from '@angular/core';
import { TimeHelper } from '../../helpers/time.helper';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'payment',
    templateUrl: './payment.component.html',
})

export class PaymentComponent {
    @Input() payment: any;
    timeHelper = new TimeHelper(this.localizationService);

    constructor(private localizationService: LocalizationService) { }
}