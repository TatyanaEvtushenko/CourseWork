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
        $("#markdownPreview").modal();
        $('div#froala-editor').froalaEditor({
            toolbarButtons: ['fullscreen', 'bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', '|',
                'fontFamily', 'fontSize', 'color', 'inlineStyle', 'paragraphStyle', '|', 'paragraphFormat', 'align', 'formatOL',
                'formatUL', 'outdent', 'indent', 'quote', '-', 'insertLink', 'insertImage', 'insertVideo', 'insertFile',
                'insertTable', '|', 'emoticons', 'specialCharacters', 'insertHR', 'selectAll', 'clearFormatting', '|', 'print',
                'help', 'html', '|', 'undo', 'redo'],
            pluginsEnabled: null
        });
    }

    change() {
        this.onChanged.emit(this.text);
    }
}