import { EventEmitter } from '@angular/core'; 
import { CurrentUserService } from '../services/currentuser.service';
import { AccountService } from "../services/account.service";
import { CurrentUserSubscriber } from './currentuser.subscriber';
import { UserMessage } from "../viewmodels/message";
import { MessageSenderService } from "../services/messagesender.service";
import { CurrentUser } from "../viewmodels/currentuser";
import { UserInfo } from "../viewmodels/userinfo";
declare var Materialize: any;

export class MessageSubscriber extends CurrentUserSubscriber {
	messages = new EventEmitter<UserMessage[]>();

	constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService, protected messageSenderService: MessageSenderService) {
		super(currentUserService, accountService);
		//this.isInitialized.subscribe(() => {
		//	this.getConfirmationRequests();
		//});
		this.subscribeToMessages();
		this.updateMessages();
	}

	private updateMessages() {
		this.currentUserService.updateMessages().subscribe((messages: UserMessage[]) => {
			this.messages.emit(messages);
		});
	}

	private subscribeToMessages() {
		this.messages.subscribe((messages: UserMessage[]) => {
			let ids = this.getIds(messages);
			messages.forEach((message) => {
				Materialize.toast(message.text, 60000);
			});
			this.currentUserService.markAsRead(ids).subscribe((data: void) => {});
		});
	}

	private getConfirmationRequests() {
		if (this.isAdmin) {
			this.accountService.getFilteredUserList({ unconfirmed: false, requested: true, confirmed: false }).subscribe(
				(userInfos: UserInfo[]) => {
					if (userInfos.length > 0) {
						var generatedText = this.generateConfirmationMessageText(userInfos);
						Materialize.toast(generatedText, 60000);
						//this.messageSenderService.sendMessage([{ recipientUserName: this.currentUser.userName, text: generatedText }]).
						//	subscribe((data: void) => { });
					}
				});
		}
	}

	private generateConfirmationMessageText(userInfos: UserInfo[]) {
		var text = "";
		let ending = '<br> have requested account confirmation. <a href="/AdminPage?confirmed=false?unconfirmed=false?requested=true"> &nbsp; Go to admin page </a>';
		text = text.concat(userInfos[0].username);
		if (userInfos.length == 1) return text.concat(ending);
		userInfos.forEach((item, index) => {
			if (index == 0) return;
			if (index >= 3) return;
			text = text.concat(',<br>' + item.username);
		});
		if (userInfos.length <= 3) return text.concat(ending);
		return text.concat(' and <br>' + (userInfos.length - 3) + ' other users ' + ending);
	}

	private getIds(messages: UserMessage[]) {
		var result: string[] = [];
		messages.forEach((message) => {
			result.push(message.id);
		});
		return result;
	}
}