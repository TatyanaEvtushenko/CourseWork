import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { MessageSubscriber } from '../message.subscriber';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent extends MessageSubscriber {

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService) {
        super(currentUserService, accountService);
        title.setTitle("My projects");
    }
}