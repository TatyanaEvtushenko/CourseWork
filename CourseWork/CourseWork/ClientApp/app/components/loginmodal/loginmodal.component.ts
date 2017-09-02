import { Component, AfterViewInit } from '@angular/core';
declare var $: any;

@Component({
    selector: 'loginmodal',
    templateUrl: './loginmodal.component.html'
})
export class LoginModalComponent implements AfterViewInit {
    ngAfterViewInit() {
        $('#loginModal').modal();
    }
}