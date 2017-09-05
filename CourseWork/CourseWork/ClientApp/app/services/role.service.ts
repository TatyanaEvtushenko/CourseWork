import { RoleNames } from '../viewmodels/roleNames';
import { CurrentUser } from '../viewmodels/currentuser';

export class RoleService {
    roles = new RoleNames();

    isUser(user: CurrentUser) {
        return user != null;
    }

    isAdmin(user: CurrentUser) {
        return this.isUser(user) && user.role === this.roles.admin;
    }

    isConfirmedUser(user: CurrentUser) {
        return this.isUser(user) && (this.isAdmin(user) || user.role === this.roles.confirmedUser);
    }

    isJustUser(user: CurrentUser) {
        return this.isUser(user) && user.role === this.roles.user;
    }
}