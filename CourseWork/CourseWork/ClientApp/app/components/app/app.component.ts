import { Component } from '@angular/core';
import { StorageService } from '../../services/storage.service';
import { AccountService } from "../../services/account.service";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent {
    constructor(public storage: StorageService, private accountService: AccountService) { }

    logout() {
        this.accountService.logout().subscribe(
            data => this.accountService.changeAuthState(false)
        );
    }
 }