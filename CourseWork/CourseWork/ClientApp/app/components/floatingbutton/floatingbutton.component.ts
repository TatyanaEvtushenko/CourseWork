import { Component } from '@angular/core';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';

@Component({
    selector: 'floatingbutton',
    templateUrl: './floatingbutton.component.html',
})
export class FloatingButtonComponent extends CurrentUserSubscriber {

    constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService) {
        super(currentUserService, accountService);
    }
}