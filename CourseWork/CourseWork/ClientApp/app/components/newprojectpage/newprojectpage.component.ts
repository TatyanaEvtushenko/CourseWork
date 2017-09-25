import { Component } from '@angular/core';
import { MessageSubscriberService } from '../../services/messagesubscriber.service';
declare var $: any;

@Component({
    selector: 'newprojectpage',
    templateUrl: './newprojectpage.component.html',
})

export class NewProjectPageComponent{

    constructor(public storage: MessageSubscriberService) { }
}