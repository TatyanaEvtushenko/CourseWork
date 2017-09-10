import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { MessageSubscriber } from '../message.subscriber';
import { MessageSenderService } from "../../services/messagesender.service";

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent extends MessageSubscriber {

	constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, protected messageSenderService: MessageSenderService) {
        super(currentUserService, accountService, messageSenderService);
        title.setTitle("My projects");
    }
}