import { Component, AfterViewInit, Input} from '@angular/core';
import { ProjectService } from "../../services/project.service";
declare var $: any;
declare var Materialize: any;

@Component({
    selector: 'paymentmodal',
    templateUrl: './paymentmodal.component.html'
})
export class PaymentModalComponent implements AfterViewInit {
    @Input() projectId: string;
    paymentForm: any = {};
    paymentInfo: any = {};
    isWrongRequest = false;

    constructor(private projectService: ProjectService) { }

    ngAfterViewInit() {
        $('#paymentModal').modal({
            ready: (modal: any, trigger: any) => this.getPaymentInfo(),
            complete: this.paymentForm = this.paymentInfo = {}
        });
    }

    onSubmit() {
        if (this.isReadyFormToSent()) {
            this.projectService.addPayment(this.paymentForm).subscribe(
                data => this.getResponse(data),
                error => this.isWrongRequest = true
            );
        }
    }

    private isReadyFormToSent() {
        return this.paymentForm.paidAmount != null && this.paymentForm.paidAmount !== "" &&
            this.paymentForm.accountNumber != null && this.paymentForm.accountNumber !== "" &&
            this.paymentForm.paidAmount >= this.paymentInfo.minPaymentAmount && this.paymentForm.paidAmount <= this.paymentInfo.maxPaymentAmount;
    }

    private getPaymentInfo() {
        this.projectService.getPaymentInfoForForm(this.projectId).subscribe(
            data => this.prepareData(data));
    }

    private prepareData(data: any) {
        this.paymentInfo = data;
        this.paymentForm.accountNumber = data.keptAccountNumber;
        this.paymentForm.projectId = this.projectId;
    }

    private getResponse(data: any) {
        this.isWrongRequest = !data;
        if (!this.isWrongRequest) {
            Materialize.toast('Payment is carried out successfully.', 4000);
            location.reload();
        }
    }
}