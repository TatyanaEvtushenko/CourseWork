import { Component, Input, Output, EventEmitter } from '@angular/core';
declare var $: any;

@Component({
    selector: 'markdowneditor',
    templateUrl: './markdowneditor.component.html',
})

export class MarkdownEditorComponent {
    @Input() fieldName: string;
    @Input() text = "";
    @Output() onChanged = new EventEmitter<string>();
    options: any = null;

    ngOnInit() {
        var self = this;
        this.options = {
            placeholder: this.fieldName,
            toolbarButtons: ['fullscreen', 'bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', '|',
                'fontFamily', 'fontSize', 'color', 'inlineStyle', 'paragraphStyle', '|', 'paragraphFormat', 'align', 'formatOL',
                'formatUL', 'outdent', 'indent', 'quote', '-', 'insertLink', 'insertImage', 'insertVideo', 'insertFile',
                'insertTable', '|', 'emoticons', 'specialCharacters', 'insertHR', 'selectAll', 'clearFormatting', '|', 'print',
                'help', 'html', '|', 'undo', 'redo'],
            events: {
                'froalaEditor.contentChanged'(e: any, editor: any) {
                    self.onChanged.emit(self.text);
                }
            }
        }
    }
}