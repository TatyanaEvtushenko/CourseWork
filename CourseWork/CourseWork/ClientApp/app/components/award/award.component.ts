import { Component, Input } from '@angular/core';
import { AwardService } from "../../services/award.service";

@Component({
    selector: 'award',
    templateUrl: './award.component.html',
})

export class AwardComponent {
    @Input() award: any;

    constructor(private awardService: AwardService) { }
}