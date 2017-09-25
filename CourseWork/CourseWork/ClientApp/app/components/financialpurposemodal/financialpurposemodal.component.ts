import { Component, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { FinancialPurpose } from '../../viewmodels/financialpurpose';
import { LocalizationService } from "../../services/localization.service";
declare var $: any;

@Component({
    selector: 'financialpurposemodal',
    templateUrl: './financialpurposemodal.component.html'
})
export class FinancialPurposeModalComponent implements AfterViewInit {
    financialPurpose = new FinancialPurpose();
    @Output() onCreated = new EventEmitter<FinancialPurpose>();
    keys = ["NAME", "DESCR", "BUDGET"];
    translations = {}

    constructor(private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    ngAfterViewInit() {
        $('#financialPurposeModal').modal();
        $('#financialPurpose-budget').characterCounter();
    }

    onSubmit() {
        if (this.financialPurpose.name != null && this.financialPurpose.budget != null) {
            this.onCreated.emit(this.financialPurpose);
            $('#financialPurposeModal').modal("close");
            this.financialPurpose = new FinancialPurpose();
        }
    }
}
