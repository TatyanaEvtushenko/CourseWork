import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { StorageService } from '../../services/storage.service';
import { UserInfo } from '../../viewmodels/userinfo';
import { UserStatus } from "../../enums/userstatus";
declare var $: any;

@Component({
    selector: 'adminpage',
    templateUrl: './adminpage.component.html'
})

export class AdminPageComponent {
    userInfos: UserInfo[] = [];
	isCheckedAtIndex: boolean[] = [];
	deleteWithCommentsAndRaitings = false;
    filters = { unconfirmed: true, requested: true, confirmed: true };
    userStatus = UserStatus;
    selectedIndex: number = null;
    sortOrderAscending = { "Status": true, "LastLoginTime": true };

    constructor(private title: Title, public storage: StorageService, private accountService: AccountService){
        title.setTitle("Admin page");
    }

    ngOnInit() {
        this.accountService.getUserList().subscribe(userInfos => {
            this.isCheckedAtIndex = new Array<boolean>(userInfos.length);
            this.userInfos = userInfos;
        });
    }

    filter() {
        this.accountService.getFilteredUserList(this.filters).subscribe(userInfos => {
            this.isCheckedAtIndex = new Array<boolean>(userInfos.length);
            this.userInfos = userInfos;
        });
    }

    clickConfirm() {
        $('#adminConfirmationModal').modal("open");
    }

    changeStatus(accept: boolean) {
        if (accept) {
            this.userInfos[this.selectedIndex].statusCode = UserStatus.Confirmed;
            this.userInfos[this.selectedIndex].status = "Confirmed"; 
        } else {
            this.userInfos[this.selectedIndex].statusCode = UserStatus.WithoutConfirmation;
            this.userInfos[this.selectedIndex].status = "Without confirmation";
        }
    }

    sortByField(fieldName: string) {
        this.accountService.sortByField(fieldName, this.sortOrderAscending[fieldName]).subscribe(userInfos => {
            this.userInfos = userInfos;
            this.sortOrderAscending[fieldName] = !this.sortOrderAscending[fieldName];
            this.isCheckedAtIndex = new Array<boolean>(this.userInfos.length);
        });
    }

    delete() {
        this.accountService.delete(this.getSelectedUsers(), this.deleteWithCommentsAndRaitings).subscribe((success) => {
            if (success) {
                var result: UserInfo[] = [];
                this.userInfos.forEach((item, index) => {
                    if (!this.isCheckedAtIndex[index])
                        result.push(item);
                });
                this.userInfos = result;
                this.isCheckedAtIndex = new Array<boolean>(this.userInfos.length);
            }
        });
    }

    blockUnblock() {
        this.accountService.blockUnblock(this.getSelectedUsers()).subscribe((success) => {
            if (success) {
                this.userInfos.forEach((item, index) => {
                    if (this.isCheckedAtIndex[index])
                        item.isBlocked = !item.isBlocked;
                });
                this.isCheckedAtIndex = new Array<boolean>(this.userInfos.length);
            }
        });
    }

    private getSelectedUsers() {
        var result: string[] = [];
        this.userInfos.forEach((item, index) => {
            if (this.isCheckedAtIndex[index])
                result.push(item.username);
        });
        return result;
    }
}