import { Component } from '@angular/core';
import { StorageService } from '../../services/storage.service';
import { AccountService } from "../../services/account.service";
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent {
    keys = ["Register", "LogIn", "LogOut", "AdminPage", "MyPage", "ConfirmYourAccount"];
    translations = {};

    constructor(public storage: StorageService,
        private accountService: AccountService,
        private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe(data => {
            this.translations = data;
        })
    }

    logout() {
        this.accountService.logout().subscribe(
            data => this.accountService.changeAuthState(false)
        );
    }
 }