import { Component } from '@angular/core';
import {Title} from '@angular/platform-browser';
import { MessageSubscriber } from "../message.subscriber";
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from "../../services/currentuser.service";

@Component({
    selector: 'homepage',
    templateUrl: './homepage.component.html'
})
export class HomePageComponent extends MessageSubscriber {
	constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService) {
		super(currentUserService, accountService);
        title.setTitle("Home page");
    }
}
