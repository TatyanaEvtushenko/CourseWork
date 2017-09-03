import { Component, Input } from '@angular/core';
import { CurrentUser } from '../../viewmodels/currentuser';
import { AccountService } from "../../services/account.service";

@Component({
    selector: 'pagelinks',
    templateUrl: './pagelinks.component.html'
})
export class PageLinksComponent {
    @Input("currentUser") currentUser: CurrentUser;

    constructor(private accountService: AccountService) { }

    logout() {   
        this.accountService.logout().subscribe(
            (data: void) => {
                this.accountService.changeAuthState(false);
            }
        );
    }
}