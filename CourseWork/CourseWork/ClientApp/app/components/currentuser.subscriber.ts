import { CurrentUser } from '../viewmodels/currentuser';
import { AccountService } from "../services/account.service";
import { CurrentUserService } from '../services/currentuser.service';
import { RoleService } from '../services/role.service';

export class CurrentUserSubscriber {
    currentUser: CurrentUser = null;
    isUser = false;
    isAdmin = false;
    isConfirmedUser = false;
    isJustUser = false;

    constructor(protected currentUserService: CurrentUserService, protected accountService: AccountService, protected roleService: RoleService) {
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

    private updateData(user: CurrentUser) {
        this.currentUser = user;
        this.updateRoles();  
    }

    private updateRoles() {
        this.isUser = this.roleService.isUser(this.currentUser);
        this.isAdmin = this.roleService.isAdmin(this.currentUser);
        this.isConfirmedUser = this.roleService.isConfirmedUser(this.currentUser);
        this.isJustUser = this.roleService.isJustUser(this.currentUser);
    }
}