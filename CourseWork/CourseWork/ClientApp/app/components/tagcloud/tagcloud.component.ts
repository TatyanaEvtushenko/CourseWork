import { Component } from '@angular/core';
import { CloudData } from 'angular-tag-cloud-module';
import { TagService } from '../../services/tag.service';

@Component({
    selector: 'tagcloud',
    templateUrl: './tagcloud.component.html',
})
export class TagCloudComponent {

    data: Array<CloudData> = [];
     
    constructor(private tagService: TagService) { }
     
    ngOnInit() {
        this.tagService.getTagCloud().subscribe(
            (data) => this.data = data.map((x: any) => {
                return {
                    text: x.name,
                    weight: x.numberOfUsing,
                    link: '/SearchResult?searchQuery=' + x.name
            }
            }),
            (error) => this.data = []
        );
    }
}