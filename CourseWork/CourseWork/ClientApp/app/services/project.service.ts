import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { NewProjectForm } from "../viewmodels/newprojectform";
import { NewsForm } from '../viewmodels/newsform';

@Injectable()
export class ProjectService extends BaseService {

    addProject(projectForm: NewProjectForm) {
        return this.requestPost("api/Project/AddProject", projectForm);
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

    addMailingToSubscribers(newsForm: NewsForm) {
        return this.requestPost("api/News/AddMailingToSubscribers", newsForm);
    }

    addMailingToPayers(newsForm: NewsForm) {
        return this.requestPost("api/News/AddMailingToPayers", newsForm);
    }
}