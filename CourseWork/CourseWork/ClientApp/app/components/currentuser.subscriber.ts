import { EventEmitter } from '@angular/core';
import { CurrentUser } from '../viewmodels/currentuser';
import { AccountService } from "../services/account.service";
import { CurrentUserService } from '../services/currentuser.service';

export class CurrentUserSubscriber {
    currentUser: CurrentUser = null;
    isReadyCurrentUser = false;
    isUser = false;
    isAdmin = false;
    isConfirmedUser = false;
	isJustUser = false;
	isInitialized = new EventEmitter();

    constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService) {
        this.subscribeCurrentUser();
        this.subscribeAccount();
        this.currentUserService.getCurrentUser();
    }

    private subscribeCurrentUser() {
        this.currentUserService.isReady.subscribe((user: CurrentUser) => {
            this.updateData(user);
        });
    }

    private subscribeAccount() {
        this.accountService.isAuthChanged.subscribe((isLoggedIn: boolean) => {
            if (isLoggedIn) {
                this.currentUserService.getCurrentUser();
            } else {
                this.updateData(null);
            }
        });
    }

	protected updateData(user: CurrentUser) {
        this.isReadyCurrentUser = true;
        this.currentUser = user;
		this.updateRoles();
		if (user != null)
            this.updateBlockedStatus(user.isBlocked);
	    console.log(this.constructor.name);
        this.isInitialized.emit();
    }

    private updateRoles() {
        this.isUser = this.currentUser != null;
        this.isAdmin = this.isUser && this.currentUser.role === "Admin";
        this.isConfirmedUser = this.isUser && (this.isAdmin || this.currentUser.role === "ConfirmedUser");
        this.isJustUser = this.isUser && this.currentUser.role === "User";
	}

	private updateBlockedStatus(isBlocked: boolean) {
		if (isBlocked)
			this.accountService.changeAuthState(false);
	}
}