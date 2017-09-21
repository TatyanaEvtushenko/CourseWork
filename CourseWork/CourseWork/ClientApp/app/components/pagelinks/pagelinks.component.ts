import { Component } from '@angular/core';
import { AccountService } from "../../services/account.service";
import { StorageService } from '../../services/storage.service';
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'pagelinks',
    templateUrl: './pagelinks.component.html'
})

export class PageLinksComponent {
    keys = ['Hello', 'MyPage', 'AdminPage', 'ConfirmYourAccount', 'LogOut', 'Register', 'LogIn'];
    translations = {};

    constructor(public storage: StorageService, private accountService: AccountService, private localizationService: LocalizationService) { }

    logout() {   
        this.accountService.logout().subscribe(
            data  => this.accountService.changeAuthState(false)
        );
    }

    ngOnInit() {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }
}