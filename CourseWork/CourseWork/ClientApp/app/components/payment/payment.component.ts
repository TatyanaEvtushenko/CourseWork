import { Component, Input } from '@angular/core';
import { TimeHelper } from '../../helpers/time.helper';

@Component({
    selector: 'payment',
    templateUrl: './payment.component.html',
})

export class PaymentComponent {
    @Input() payment: any;
    timeHelper = new TimeHelper();
}