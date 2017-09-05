import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { RoleService } from '../../services/role.service';
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent extends CurrentUserSubscriber {

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, protected roleService: RoleService) {
        super(currentUserService, accountService, roleService);
        title.setTitle("My projects");
    }
}