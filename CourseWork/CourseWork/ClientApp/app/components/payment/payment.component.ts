import { Component, Input } from '@angular/core';
import { TimeService } from '../../services/time.service';

@Component({
    selector: 'payment',
    templateUrl: './payment.component.html',
})

export class PaymentComponent {
    @Input() payment: any;

    constructor(public timeService: TimeService) { }
}