import { Component, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
declare var $: any;

@Component({
    selector: 'markdowneditor',
    templateUrl: './markdowneditor.component.html',
})

export class MarkdownEditorComponent implements AfterViewInit {
    @Input() fieldName: string;
    @Output() onChanged = new EventEmitter<string>();
    text = "";

    ngAfterViewInit() {
        $('#markdownPreview').modal();
    }

    change() {
        this.onChanged.emit(this.text);
    }
}