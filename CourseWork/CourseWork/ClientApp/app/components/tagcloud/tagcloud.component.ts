import { Component } from '@angular/core';
import { CloudData, CloudOptions } from 'angular-tag-cloud-module';
import { Response} from '@angular/http';
import { TagService } from '../../services/tag.service';

@Component({
    selector: 'tagcloud',
    templateUrl: './tagcloud.component.html',
})
export class TagCloudComponent {

    data: Array<CloudData> = [];
     
    constructor(private tagService: TagService) { }
     
    ngOnInit() {
        this.tagService.getTagCloud().subscribe((resp: Response) => {
            this.data = resp.json().map(x => {
                 return { text: x.name, weight: x.numberOfUsing, link: '/' }
            });
        });
    }
}