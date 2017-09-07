import { Component, EventEmitter, Output } from '@angular/core';
import { TagService } from '../../services/tag.service';
declare var $: any;

@Component({
    selector: 'tagsearcher',
    templateUrl: './tagsearcher.component.html',
})

export class TagSearcherComponent {
    data: string[] = [];
    autocompleteInit: any;
    @Output() onChanged = new EventEmitter<string[]>();
    
    constructor(private tagService: TagService) {
        tagService.getTags().subscribe((data) => {
            this.autocompleteInit = {
                autocompleteOptions: {
                    data: this.convertToData(data)
                }
            }
        });
    }
    
    add(chip: any) {
        this.data.push(chip.tag);
        this.change();
    }

    delete(chip: any) {
        const index = this.data.indexOf(chip.tag);
        if (index >= 0) {
            this.data.splice(index, 1);
        }
        this.change();
    }

    private convertToData(data: string[]) {
        const autocompleteData: any = [];
        for (let tag of data) {
            autocompleteData[tag] = null;
        }
        return autocompleteData; 
    }

    private change() {
        const tags = [];
        for (let i = 0; i < this.data.length; i += 2) {
            tags.push(this.data[i]);
        }
        this.onChanged.emit(tags);
    }
}