import { Injectable } from '@angular/core';
import { CurrentUserService } from '../services/currentuser.service';
import { AccountService } from "../services/account.service";


@Injectable()
export class StorageService {
    currentUser: any = null;
    isReadyCurrentUser = false;
    isUser = false;
    isAdmin = false;
    isConfirmedUser = false;
    isJustUser = false;

    constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService) {
        this.getCurrentUser();
        this.subscribeForEvents();
    }

    private getCurrentUser() {
        this.currentUserService.getCurrentUser().subscribe(
            data => this.updateData(data),
            error => this.updateData(null)
        );
    }

    private subscribeForEvents() {
        this.accountService.isAuthChanged.subscribe((isLoggedIn: boolean) => {
            this.isReadyCurrentUser = false;
            if (isLoggedIn) {
                this.getCurrentUser();
            } else {
                this.updateData(null);
            }
        });
    }

    private updateData(user: any) {
        this.currentUser = user;
        this.updateRoles();
        if (user != null) {
            this.updateBlockedStatus(user.isBlocked);
        }
        this.isReadyCurrentUser = true;
    }

    private updateRoles() {
        this.isUser = this.currentUser != null;
        this.isAdmin = this.isUser && this.currentUser.role === "Admin";
        this.isConfirmedUser = this.isUser && (this.isAdmin || this.currentUser.role === "ConfirmedUser");
        this.isJustUser = this.isUser && this.currentUser.role === "User";
    }

    private updateBlockedStatus(isBlocked: boolean) {
        if (isBlocked) {
            this.accountService.logout().subscribe(
                data => this.accountService.changeAuthState(false)
            );
        }
    }
}