import { Component, Input } from '@angular/core';

@Component({
    selector: 'pagelinks',
    templateUrl: './pagelinks.component.html'
})
export class PageLinksComponent {
    @Input("currentUserName") userName:string;
    @Input("isAuthenticated") isAuthenticated:boolean;
}