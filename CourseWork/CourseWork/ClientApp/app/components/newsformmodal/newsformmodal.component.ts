import { Component, AfterViewInit, Input } from '@angular/core';
import { NewsForm } from '../../viewmodels/newsform';
import { ProjectService } from "../../services/project.service";
declare var $: any;

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

    constructor(private projectService: ProjectService) { }

    ngAfterViewInit() {
        $('#newsModal').modal();
    }

    onSubmit() {
        //if (this.loginForm.email != null && this.loginForm.password != null) {
        //    this.accountService.login(this.loginForm).subscribe(
        //        (data) => this.getResponse(data),
        //        (error) => this.isWrongRequest = true
        //    );
        //}
    }

    private getResponse(data: any) {
        //this.isWrongRequest = !data;
        //if (!this.isWrongRequest) {
        //    $('#loginModal').modal("close");
        //    this.accountService.changeAuthState(true);
        //}
    }
}