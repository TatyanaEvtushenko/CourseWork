import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';

@Component({
    selector: 'adminpage',
    templateUrl: './adminpage.component.html'
})

export class AdminPageComponent extends CurrentUserSubscriber {

    constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService) {
        super(currentUserService, accountService);
        title.setTitle("Admin page");
    }
}