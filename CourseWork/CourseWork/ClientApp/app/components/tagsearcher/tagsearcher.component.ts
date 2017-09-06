import { Component, AfterViewInit, Output, EventEmitter } from '@angular/core';
import { TagService } from '../../services/tag.service';
declare var $: any;

@Component({
    selector: 'tagsearcher',
    templateUrl: './tagsearcher.component.html',
})

export class TagSearcherComponent implements AfterViewInit {
    @Output() onChanged = new EventEmitter<string>();
    data: Array<any> = [];
     
    constructor(private tagService: TagService) { }
     
    ngOnInit() {
        this.tagService.getTagCloud().subscribe(
            (data) => {
                $('input.autocomplete').autocomplete({
                    data: data.map((x: any) => { return { text: x.name, weight: x.numberOfUsing, link: '/' } }),
                    onAutocomplete(tag: string) {
                        // Callback function when value is autcompleted.
                    },
                });
            },
            (error) => this.data = []
        );
    }

    ngAfterViewInit() {
    }

    change() {
    }
}