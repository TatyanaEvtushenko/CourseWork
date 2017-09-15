import { Component, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
declare var $: any;

@Component({
    selector: 'markdowneditor',
    templateUrl: './markdowneditor.component.html',
})

export class MarkdownEditorComponent implements AfterViewInit {
    @Input() fieldName: string;
    @Input() text = "";
    @Output() onChanged = new EventEmitter<string>();

    ngAfterViewInit() {
        $('#markdownPreview').modal();
    }

    change() {
        this.onChanged.emit(this.text);
    }
}