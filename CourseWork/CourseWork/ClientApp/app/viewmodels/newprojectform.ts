import {FinancialPurpose} from "./financialpurpose";

export class NewProjectForm{
    name: string;
    fundRaisingEnd: Date;
    description: string;
    imageUrl: string;
    financialPurposes: FinancialPurpose[];
    tags: string[];
}