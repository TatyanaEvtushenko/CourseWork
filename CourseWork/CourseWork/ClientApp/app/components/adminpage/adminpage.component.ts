import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from "../../services/account.service";
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
import { UserInfo } from '../../viewmodels/userinfo';
import { UserStatus } from "../../enums/userstatus";
import { MessageSenderService } from "../../services/messagesender.service";
import { LocalizationService } from "../../services/localization.service";
declare var $: any;
declare var Materialize: any;

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
    keys = ["VIEWCONFIRMATIONREQUEST", "STATUS", "LASTLOGINTIME", "USERNAME", "REGISTRATIONTIME", "PROJECTNUMBER", "RATING", "SELECTUSER",
        "AWAITING", "UNCONFIRMED", "CONFIRMED", "SHOWCONFIRMED", "SHOWUNCONFIRMED", "SHOWREQUESTED", "AdminPage", "FILTER",
        "DELETESELECTED", "BLOCKSELECTED", "DELCOMMENTSRATINGS", "APPROVECONFIRMATION", "DECLINECONFIRMATION"];
    translations = {};

    constructor(private title: Title, public storage: MessageSubscriberService, private accountService: AccountService,
        private route: ActivatedRoute, private router: Router, protected messageSenderService: MessageSenderService,
        private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
            title.setTitle(this.translations['AdminPage']);
        });
    }

	ngOnInit() {
		this.route.queryParams.subscribe(params => {
			this.filters.confirmed = !(params["confirmed"] == 'false');
			this.filters.unconfirmed = !(params["unconfirmed"] == 'false');
			this.filters.requested = !(params["requested"] == 'false');
			this.accountService.getFilteredUserList(this.filters).subscribe(userInfos => {
				this.isCheckedAtIndex = new Array<boolean>(userInfos.length);
				this.userInfos = userInfos;
			});
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
			this.userInfos[this.selectedIndex].status = "CONFIRMED"; 
        } else {
            this.userInfos[this.selectedIndex].statusCode = UserStatus.WithoutConfirmation;
            this.userInfos[this.selectedIndex].status = "UNCONFIRMED";
		}
	    this.sendConfirmationMessage(this.userInfos[this.selectedIndex].username, this.generateResponseMessage(accept));
    }

    sortByField(fieldName: string) {
        this.accountService.sortByField(fieldName, this.sortOrderAscending[fieldName], this.filters).subscribe(userInfos => {
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

	private sendConfirmationMessage(username: string, message: string) {
		this.messageSenderService.sendMessage([{ recipientUserName: username, text: message }]).
			subscribe((data: void) => { });
	}

	private generateResponseMessage(accept: boolean) {
        return accept ? this.translations["APPROVECONFIRMATION"] : this.translations['DECLINECONFIRMATION'];
	}
}