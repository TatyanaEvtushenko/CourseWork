import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { RoleService } from '../../services/role.service';
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { UserInfo } from '../../viewmodels/userinfo';

@Component({
    selector: 'adminpage',
    templateUrl: './adminpage.component.html'
})

export class AdminPageComponent extends CurrentUserSubscriber {

    tests = ['test1', 'test2']; 

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, protected roleService: RoleService) {
        super(currentUserService, accountService, roleService);
        title.setTitle("Admin page");
    }
}