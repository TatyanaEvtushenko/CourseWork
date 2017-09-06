import { Component, AfterViewInit } from '@angular/core';
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
    }

    onSubmit() {
        console.log(this.projectForm);
        this.projectService.addProject(this.projectForm).subscribe(
            (data) => this.isWrongRequest = !data,
            (error) => this.isWrongRequest = true
        );
    }
}