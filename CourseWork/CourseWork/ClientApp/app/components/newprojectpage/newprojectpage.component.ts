import { Component, AfterViewInit } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { RoleService } from '../../services/role.service';
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
declare var $: any;

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent extends CurrentUserSubscriber implements AfterViewInit {
    projectForm = new NewProjectForm();
    isWrongRequest = false;

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, protected roleService: RoleService) {
        super(currentUserService, accountService, roleService);
        title.setTitle("New project");
    }

    ngAfterViewInit() {
        $('.datepicker').pickadate({
            selectMonths: true, // Creates a dropdown to control month
            selectYears: 15, // Creates a dropdown of 15 years to control year,
            today: 'Today',
            clear: 'Clear',
            close: 'Ok',
            closeOnSelect: false // Close upon selecting a date,
        });
    }

    onSubmit() {
        //this.accountService.login(this.loginForm).subscribe(
        //    (data) => {
        //        this.isWrongRequest = !data;
        //        if (!this.isWrongRequest) {
        //            $('#loginModal').modal("close");
        //            this.accountService.changeAuthState(true);
        //        }
        //    },
        //    (error) => this.isWrongRequest = true
        //);
    }
}