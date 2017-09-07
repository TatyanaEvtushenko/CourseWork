import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FinancialPurpose } from '../../viewmodels/financialpurpose';
declare var $: any;

@Component({
    selector: 'financialpurpose',
    templateUrl: './financialpurpose.component.html',
})

export class FinancialPurposeComponent {
    @Input() isOwner: boolean;
    @Input() financialPurpose: FinancialPurpose;
    @Output() onDeleted = new EventEmitter();

    delete() {
        this.onDeleted.emit();
    }
}