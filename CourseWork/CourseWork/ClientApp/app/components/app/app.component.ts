import { Component } from '@angular/core';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
declare var Materialize: any;

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent extends CurrentUserSubscriber {

    constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService) {
		super(currentUserService, accountService);
	}
 }