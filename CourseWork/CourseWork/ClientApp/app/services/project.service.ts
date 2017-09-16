import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { NewProjectForm } from "../viewmodels/newprojectform";
import { NewsForm } from '../viewmodels/newsform';

@Injectable()
export class ProjectService extends BaseService {

    addProject(projectForm: NewProjectForm) {
        return this.requestPost("api/Project/AddProject", projectForm);
    }

    updateProject(projectForm: NewProjectForm) {
        return this.requestPost("api/Project/UpdateProject", projectForm);
    }

    getProjectEditorForm(id: string) {
        return this.requestGet(`api/Project/GetProjectEditorForm/${id}`);
    }

    subscribe(projectId: string) {
        return this.requestPost("api/Subscriber/Subscribe", projectId);
    }

    unsubscribe(projectId: string) {
        return this.requestPost("api/Subscriber/Unsubscribe", projectId);
    }

    getUserProjects() {
        return this.requestGet("api/Project/GetUserProjects");
    }

    getProject(id: string) {
        return this.requestGet(`api/Project/GetProject/${id}`);
    }

    addNews(newsForm: NewsForm) {
        return this.requestPost("api/News/AddNews", newsForm);
    }

    deleteNews(newsId: string) {
        return this.requestPost("api/News/RemoveNews", newsId);
    }

    addMailingToSubscribers(newsForm: NewsForm) {
        return this.requestPost("api/News/AddMailingToSubscribers", newsForm);
    }

    addMailingToPayers(newsForm: NewsForm) {
        return this.requestPost("api/News/AddMailingToPayers", newsForm);
    }

    changeRating(projectId: string, ratingValue: number) {
        const rating = { "projectId": projectId, "ratingValue": ratingValue };
        return this.requestPost("api/Project/ChangeRating", rating);   
    }
}