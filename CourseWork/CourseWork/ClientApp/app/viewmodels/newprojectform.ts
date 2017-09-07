import {FinancialPurpose} from "./financialpurpose";

export class NewProjectForm{
    name: string;
    fundRaisingEnd: Date;
    description: string;
    image: string;
    financialPurposes: FinancialPurpose[];
    tags: string[];
    minPaymentAmount: number;
    maxPaymentAmount: number;
}