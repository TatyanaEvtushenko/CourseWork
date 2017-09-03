import { Component, Input } from '@angular/core';
import { CurrentUser } from '../../viewmodels/currentuser';

@Component({
    selector: 'pagelinks',
    templateUrl: './pagelinks.component.html'
})
export class PageLinksComponent {
    @Input("currentUser") currentUser: CurrentUser;
}