import { Component } from '@angular/core';
import { AccountService } from "../../services/account.service";
import { StorageService } from '../../services/storage.service';

@Component({
    selector: 'pagelinks',
    templateUrl: './pagelinks.component.html'
})

export class PageLinksComponent {

    constructor(public storage: StorageService, private accountService: AccountService) { }

    logout() {   
        this.accountService.logout().subscribe(
            data  => this.accountService.changeAuthState(false)
        );
    }
}