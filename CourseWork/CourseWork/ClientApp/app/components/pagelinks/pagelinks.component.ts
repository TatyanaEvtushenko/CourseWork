import { Component } from '@angular/core';
import { AccountService } from "../../services/account.service";
import { RoleService } from '../../services/role.service';
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';

@Component({
    selector: 'pagelinks',
    templateUrl: './pagelinks.component.html'
})

export class PageLinksComponent extends CurrentUserSubscriber {

    constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService, protected roleService: RoleService) {
        super(currentUserService, accountService, roleService);
    }

    logout() {   
        this.accountService.logout().subscribe(
            (data: void) => {
                this.accountService.changeAuthState(false);
            }
        );
    }
}