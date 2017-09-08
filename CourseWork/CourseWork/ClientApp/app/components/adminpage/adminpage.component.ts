import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { UserInfo } from '../../viewmodels/userinfo';
import { UserStatus } from "../../enums/userstatus";
declare var $: any;

@Component({
    selector: 'adminpage',
    templateUrl: './adminpage.component.html'
})

export class AdminPageComponent extends CurrentUserSubscriber {
    userInfos: UserInfo[] = [];
    filters = { unconfirmed: true, requested: true, confirmed: true };
    userStatus = UserStatus;
    selectedIndex: number = null;

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService) {
        super(currentUserService, accountService);
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

    clickConfirm() {
        $('#adminConfirmationModal').modal("open");
    }

    changeStatus(accept: boolean) {
        console.log(accept);
        if (accept) {
            this.userInfos[this.selectedIndex].statusCode = UserStatus.Confirmed;
            this.userInfos[this.selectedIndex].status = "Confirmed"; 
        } else {
            this.userInfos[this.selectedIndex].statusCode = UserStatus.WithoutConfirmation;
            this.userInfos[this.selectedIndex].status = "Without confirmation";
        }
        
    }
}