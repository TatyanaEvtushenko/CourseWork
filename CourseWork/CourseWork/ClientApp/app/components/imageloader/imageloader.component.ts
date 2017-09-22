import { Component, EventEmitter, Output, Input } from '@angular/core';
import { LocalizationService } from "../../services/localization.service";
declare var $: any;

@Component({
    selector: 'imageloader',
    templateUrl: './imageloader.component.html'
})
export class ImageLoaderComponent {
    @Input() fieldName: string;
    @Output() emitter = new EventEmitter<string>(); 
    imageString = "";
    keys = ["BROWSE"];
    translations = {}

    constructor(private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    toBase64(file: any) {
        var reader = new FileReader();
        reader.onloadend = (e) => {
            this.previewImage(reader.result);
            this.imageString = reader.result;
            this.emitter.emit(this.imageString);
        }
        reader.readAsDataURL(file);
    }

    previewImage(file: any) {
        var preview = document.querySelector('#uploaded-image');
        preview.src = file;
    }

    onChange(event: any) {
        this.toBase64(event.srcElement.files[0]);
        
    }
} 