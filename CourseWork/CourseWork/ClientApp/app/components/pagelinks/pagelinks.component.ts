import { Component, Input } from '@angular/core';
import { CurrentUser } from '../../viewmodels/currentuser';
import { AccountService } from "../../services/account.service";
import { RoleNames } from '../../viewmodels/roleNames';

@Component({
    selector: 'pagelinks',
    templateUrl: './pagelinks.component.html'
})
export class PageLinksComponent {
    @Input("currentUser") currentUser: CurrentUser;
    @Input("roles") roles: RoleNames;

    constructor(private accountService: AccountService) { }

    logout() {   
        this.accountService.logout().subscribe(
            (data: void) => {
                this.accountService.changeAuthState(false);
            }
        );
    }

    isInRole(role: string) {
        return this.currentUser != null && this.currentUser.role === role;
    }
}