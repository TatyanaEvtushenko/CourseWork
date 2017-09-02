import { Component, AfterViewInit } from '@angular/core';
declare var $: any;

@Component({
    selector: 'registermodal',
    templateUrl: './registermodal.component.html'
})
export class RegisterModalComponent implements AfterViewInit {
    ngAfterViewInit() {
        $('#registrationModal').modal();
    }
}