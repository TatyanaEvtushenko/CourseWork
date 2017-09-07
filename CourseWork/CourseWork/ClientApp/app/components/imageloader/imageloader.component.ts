import { Component, EventEmitter, Output } from '@angular/core';
declare var $: any;

@Component({
    selector: 'imageloader',
    templateUrl: './imageloader.component.html'
})
export class ImageLoaderComponent {
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

    @Output() emitter = new EventEmitter<string>(); 
} 