import { Component, OnInit } from '@angular/core';
import { CurrentUserService } from '../../services/currentuser.service';
import { AccountService } from '../../services/account.service';
import { SettingService } from '../../services/setting.service';
import { CurrentUser } from '../../viewmodels/currentuser';
import { RoleNames } from '../../viewmodels/roleNames';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
    roleNames: RoleNames = null;
    currentUser: CurrentUser = null;

    constructor(private currentUserService: CurrentUserService,
                private accountService: AccountService,
                private settingService: SettingService) {
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
        this.getRoles();
    }

    private getRoles() {
        this.settingService.getRoleNames().subscribe((data: RoleNames) => { this.roleNames = data; console.log(this.roleNames); });
    }

    private getCurrentUser() {
        this.currentUserService.getCurrentUserInfo().subscribe((data:CurrentUser) => {this.currentUser = data; console.log(this.currentUser);});
    }

    private removeCurrentUser() {
        this.currentUser = null;
    }
}