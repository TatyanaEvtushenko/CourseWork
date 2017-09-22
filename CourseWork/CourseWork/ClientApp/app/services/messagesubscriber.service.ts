import { Injectable, EventEmitter } from '@angular/core';
import { UserMessage } from "../viewmodels/message";
import { StorageService } from "../services/storage.service";
import { CurrentUserService } from '../services/currentuser.service';
import { AccountService } from "../services/account.service";
import { UserInfo } from "../viewmodels/userinfo";
import { MessageSenderService } from "../services/messagesender.service";
import { LocalizationService } from "../services/localization.service";
declare var Materialize: any;

@Injectable()
export class MessageSubscriberService extends StorageService {
    messages = new EventEmitter<UserMessage[]>();
    subscribed = false;
    private xIconHtml = '<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet">' +
        '<div><a onclick="this.parentElement.parentElement.parentElement.removeChild(this.parentElement.parentElement)">' +
    '<i class="fa fa-times"></i></a></div>';
    keys = ["REQUESTEDCONFIRMATION", "AND", "OTHERUSERS"];
    translations = {};

    constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService, private messageSenderService: MessageSenderService, private localizationService: LocalizationService) {
        super(currentUserService, accountService);
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
            this.subscribeToMessages();
            this.getConfirmationRequests();
            this.updateMessages();
        });
    }

    private subscribeToMessages() { 
        this.messages.subscribe((messages: UserMessage[]) => {
            let ids = this.getIds(messages);
            messages.forEach((message) => {
                Materialize.toast(message.text + this.xIconHtml, 60000);
            });
            this.currentUserService.markAsRead(ids).subscribe((data: void) => {});
        });
    }

    private updateMessages() {
		this.currentUserService.updateMessages().subscribe((messages: UserMessage[]) => {
			this.messages.emit(messages);
		});
	}

	private getConfirmationRequests() {
		this.accountService.getFilteredUserList({ unconfirmed: false, requested: true, confirmed: false }).subscribe(
			(userInfos: UserInfo[]) => {
				if (userInfos.length > 0) {
					var generatedText = this.generateConfirmationMessageText(userInfos);
					this.messageSenderService.sendMessagesAsAdmin([generatedText]).
						subscribe((data: void) => {
					        this.updateMessages();
					    });
				}
			});
	}

   	private generateConfirmationMessageText(userInfos: UserInfo[]) {
        var text = "<a href=\"/AdminPage?confirmed=false&unconfirmed=false&requested=true\">";
		let ending = '<br> ' + this.translations['REQUESTEDCONFIRMATION'] + '.</a>';
		text = text.concat(userInfos[0].userName);
		if (userInfos.length == 1) return text.concat(ending);
		userInfos.forEach((item, index) => {
			if (index == 0) return;
			if (index >= 3) return;
			text = text.concat(',<br>' + item.userName);
		});
		if (userInfos.length <= 3) return text.concat(ending);
		return text.concat(this.translations['AND'] + '<br>' + (userInfos.length - 3) + this.translations['OTHERUSERS'] + ending);
	}

	private getIds(messages: UserMessage[]) {
		var result: string[] = [];
		messages.forEach((message) => {
			result.push(message.id);
		});
		return result;
	}
}