import {FinancialPurpose} from "./financialpurpose";

export class NewProjectForm{
    id: string;
    name: string;
    fundRaisingEnd: Date;
    description = "";
    imageBase64: string;
    minPaymentAmount: number;
    maxPaymentAmount: number;
    financialPurposes: FinancialPurpose[];
    tags: string[];
    accountNumber: string;
    color: string;
}