import { Component, AfterViewInit, Input } from '@angular/core';
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
                (data) => this.getResponse(data),
                (error) => this.isWrongRequest = true
            );
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