import { Component } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { ProjectService } from '../../services/project.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
declare var $: any;

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent extends CurrentUserSubscriber {
    projectForm = new NewProjectForm();
    isWrongRequest = false;

    constructor(private title: Title, 
                private projectService: ProjectService,
                protected currentUserService: CurrentUserService, 
                protected accountService: AccountService) {
        super(currentUserService, accountService);
        title.setTitle("New project");
        this.projectForm.financialPurposes = [];
    }

    getTodayDate() {
        return new Date(Date.now()).getDate();
    }

    addFinancialPurpose(purpose: any) {
        this.projectForm.financialPurposes.push(purpose);
    }

    deleteFinancialPurpose(purpose: any) {
        const index = this.projectForm.financialPurposes.indexOf(purpose);
        if (index >= 0) {
            this.projectForm.financialPurposes.splice(index, 1);
        }
    }

    onSubmit() {
        console.log(this.projectForm);
        this.projectService.addProject(this.projectForm).subscribe(
            (data) => this.isWrongRequest = !data,
            (error) => this.isWrongRequest = true
        );
    }
}