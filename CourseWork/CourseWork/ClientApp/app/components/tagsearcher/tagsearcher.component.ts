import { Component, Output, Input } from '@angular/core';
import { TagService } from '../../services/tag.service';
declare var $: any;

@Component({
    selector: 'tagsearcher',
    templateUrl: './tagsearcher.component.html',
})

export class TagSearcherComponent {
    @Input() @Output() data: string[] = [];
    autocompleteInit: any;
    
    constructor(private tagService: TagService) { }

    ngOnInit() {
        this.tagService.getTags().subscribe((data) => {
            this.autocompleteInit = {
                autocompleteOptions: { data: this.convertToAutocompeteData(data) },
                data: this.addExistedTags()
            }
        });
    }
    
    add(chip: any) {
        if (this.data.indexOf(chip.tag) < 0) {
            this.data.push(chip.tag);
        }
    }

    delete(chip: any) {
        const index = this.data.indexOf(chip.tag);
        if (index >= 0) {
            this.data.splice(index, 1);
        }
    }

    private addExistedTags() {
        const existedTags: any = [];
        for (let tag of this.data) {
            existedTags.push({ "tag": tag });
        }
        return existedTags;
    }

    private convertToAutocompeteData(data: string[]) {
        const autocompleteData: any = [];
        for (let tag of data) {
            autocompleteData[tag] = null;
        }
        return autocompleteData; 
    }
}