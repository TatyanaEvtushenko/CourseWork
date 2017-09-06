import { Component } from '@angular/core';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { RoleService } from '../../services/role.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent extends CurrentUserSubscriber {

    constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService, protected roleService: RoleService) {
        super(currentUserService, accountService, roleService);
    }
}