import { Component, OnInit } from '@angular/core';
import { CurrentUserService } from '../../services/currentuser.service';
import { AccountService } from '../../services/account.service';
import { CurrentUser } from '../../viewmodels/currentuser';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {

    currentUser: CurrentUser = null;

    constructor(private currentUserService: CurrentUserService, private accountService: AccountService) {
        this.accountService.isAuthChanged.subscribe((isLoggedIn: boolean) => {
            if (isLoggedIn) {
                this.getCurrentUser();
            } else {
                this.removeCurrentUser();
            }
        });
    }

    ngOnInit() {
        this.getCurrentUser();
    }

    private getCurrentUser() {
        this.currentUserService.getCurrentUserInfo().subscribe((data:CurrentUser) => {
            this.currentUser = data;
        });
    }

    private removeCurrentUser() {
        this.currentUser = null;
    }
}