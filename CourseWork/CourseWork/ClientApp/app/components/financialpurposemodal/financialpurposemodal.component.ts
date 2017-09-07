import { Component, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { FinancialPurpose } from '../../viewmodels/financialpurpose';
declare var $: any;

@Component({
    selector: 'financialpurposemodal',
    templateUrl: './financialpurposemodal.component.html'
})
export class FinancialPurposeModalComponent implements AfterViewInit {
    financialPurpose = new FinancialPurpose();
    @Output() onCreated = new EventEmitter<FinancialPurpose>();
    
    ngAfterViewInit() {
        $('#financialPurposeModal').modal();
        $('#financialPurpose-budget').characterCounter();
    }

    onSubmit() {
        this.onCreated.emit(this.financialPurpose);
        $('#financialPurposeModal').modal("close");
        this.financialPurpose = new FinancialPurpose();
    }
}
