import { Component, Input } from '@angular/core';

@Component({
    selector: 'loginnav',
    templateUrl: './loginnav.component.html'
})
export class LoginNavComponent {
    @Input("currentUserName") userName:string;
    @Input("isAuthenticated") isAuthenticated:boolean;
}