import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';
import { MessageSubscriber } from "../message.subscriber";
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from "../../services/currentuser.service";
import { MessageSenderService } from "../../services/messagesender.service";

@Component({
    selector: 'homepage',
    templateUrl: './homepage.component.html'
})
export class HomePageComponent extends MessageSubscriber {
	constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, protected messageSenderService: MessageSenderService) {
		super(currentUserService, accountService, messageSenderService);
        title.setTitle("Home page");
    }
}
