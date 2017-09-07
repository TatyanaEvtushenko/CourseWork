import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { RoleService } from '../../services/role.service';
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { UserInfo } from '../../viewmodels/userinfo';
declare var $: any;

@Component({
    selector: 'adminpage',
    templateUrl: './adminpage.component.html'
})

export class AdminPageComponent extends CurrentUserSubscriber {
    userInfos: UserInfo[] = [];
    filters = { unconfirmed: true, requested: true, confirmed: true };

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, protected roleService: RoleService) {
        super(currentUserService, accountService, roleService);
        title.setTitle("Admin page");
    }

    ngOnInit() {
        this.accountService.getUserList().subscribe(userInfos => {
            this.userInfos = userInfos;
        });
    }

    onSubmit() {
        this.accountService.getFilteredUserList(this.filters).subscribe(userInfos => {
            this.userInfos = userInfos;
        });
    }
}