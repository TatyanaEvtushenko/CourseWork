import { Component, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { NewsForm } from '../../viewmodels/newsform';
import { ProjectService } from "../../services/project.service";
declare var $: any;
declare var Materialize: any;

@Component({
    selector: 'newsformmodal',
    templateUrl: './newsformmodal.component.html'
})
export class NewsFormModalComponent implements AfterViewInit {
    @Input() projectId: string;
    newsForm = new NewsForm();
    isWrongRequest = false;
    isNews = true;
    isMailingToSubscribers = false;
    isMailingToPayers = false;
    isSent = false;
    @Output() onAdded = new EventEmitter<any>();

    constructor(private projectService: ProjectService) { }

    ngAfterViewInit() {
        $('#newsModal').modal();
    }

    onSubmit() {
        this.isSent = false;
        this.newsForm.projectId = this.projectId;
        this.addNews();
        this.addMailingToSubscribers();
        this.addMailingToPayers();
    }

    private addNews() {
        if (this.isNews) {
            this.projectService.addNews(this.newsForm).subscribe(
                (data) => this.getNewsResponse(data),
                (error) => this.isWrongRequest = true
            );
        }
    }

    private getNewsResponse(data: any) {
        this.getResponse(data);
        if (data) {
            this.onAdded.emit(this.newsForm);
        }
    }

    private addMailingToSubscribers() {
        if (this.isMailingToSubscribers) {
            this.projectService.addMailingToSubscribers(this.newsForm).subscribe(
                (data) => this.getResponse(data),
                (error) => this.isWrongRequest = true
            );
        }
    }

    private addMailingToPayers() {
        if (this.isMailingToPayers) {
            this.projectService.addMailingToPayers(this.newsForm).subscribe(
                (data) => this.getResponse(data),
                (error) => this.isWrongRequest = true
            );
        }
    }

    private getResponse(data: any) {
        if (!this.isSent) {
            this.isSent = true;
            this.isWrongRequest = !data;
            if (!this.isWrongRequest) {
                $('#newsModal').modal("close");
                Materialize.toast('News is sent.', 4000);
            }
        }
    }
}