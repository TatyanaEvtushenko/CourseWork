import {FinancialPurpose} from "./financialpurpose";

export class NewProjectForm{
    name: string;
    fundRaisingEnd: Date;
    description: string;
    imageBase64: string;
    minPaymentAmount: number;
    maxPaymentAmount: number;
    financialPurposes: FinancialPurpose[];
    tags: string[];
}