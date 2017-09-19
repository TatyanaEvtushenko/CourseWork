import { Component, EventEmitter, Output, Input } from '@angular/core';
declare var $: any;

@Component({
    selector: 'imageloader',
    templateUrl: './imageloader.component.html'
})
export class ImageLoaderComponent {
    @Input() fieldName: string;
    @Output() emitter = new EventEmitter<string>(); 
    imageString = "";

    toBase64(file: any) {
        var reader = new FileReader();
        reader.onloadend = (e) => {
            this.imageString = reader.result;
            this.emitter.emit(this.imageString);
        }
        reader.readAsDataURL(file);
    }

    onChange(event: any) {
        this.toBase64(event.srcElement.files[0]);
    }
} 