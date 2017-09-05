import { Component, AfterViewInit } from '@angular/core';
import { NewProjectForm } from '../../viewmodels/newprojectform';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { RoleService } from '../../services/role.service';
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { DatePickerComponent } from '../datepicker/datepicker.component';
declare var $: any;

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent extends CurrentUserSubscriber implements AfterViewInit {
    projectForm = new NewProjectForm();
    isWrongRequest = false;

    constructor(private title: Title, 
                protected currentUserService: CurrentUserService, 
                protected accountService: AccountService, 
                protected roleService: RoleService) {
        super(currentUserService, accountService, roleService);
        title.setTitle("New project");
    }

    ngAfterViewInit() {
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