import { EventEmitter } from '@angular/core'; 
import { CurrentUserService } from '../services/currentuser.service';
import { AccountService } from "../services/account.service";
import { CurrentUserSubscriber } from './currentuser.subscriber';
import { UserMessage } from "../viewmodels/message";
declare var Materialize: any;

export class MessageSubscriber extends CurrentUserSubscriber {
	messages = new EventEmitter<UserMessage[]>();

	constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService) {
		super(currentUserService, accountService);
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
			messages.forEach((message) => {
				Materialize.toast(message.text, 4000);
			});
		});
	}
}