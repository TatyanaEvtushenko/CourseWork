import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { NewProjectForm } from "../viewmodels/newprojectform";
import { NewsForm } from '../viewmodels/newsform';
import { PaymentForm } from '../viewmodels/paymentform';

@Injectable()
export class ProjectService extends BaseService {

    getLastCreatedProjects() {
        return this.requestGet("api/Project/GetLastCreatedProjects");
    }

    getFinancedProjects() {
        return this.requestGet("api/Project/GetFinancedProjects");
    }

    getBigPayments() {
        return this.requestGet("api/Payment/GetBigPayments");
    }

    getLastNews() {
        return this.requestGet("api/News/GetLastNews");
    }

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

    getProjects(username: string) {
        var params = { 'username': username }
        return this.requestGetWithParams("api/Project/GetProjects", params);
    }

    getProject(id: string) {
        return this.requestGet(`api/Project/GetProject/${id}`);
    }

    getUserSubscribedProjects() {
        return this.requestGet("api/Project/GetUserSubscribedProjects");
    }

    getSubscribedProjects(username: string) {
        var params = { 'username': username }
        return this.requestGetWithParams("api/Project/GetSubscribedProjects", params);
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

    addComment(projectId: string, text: string) {
        const comment = { "projectId": projectId, "text": text };
        return this.requestPost("api/Comment/AddComment", comment);
    }

    removeComment(commentId: string) {
        return this.requestPost("api/Comment/RemoveComment", commentId);
    }

    getPaymentInfoForForm(projectId: string) {
        return this.requestGet(`api/Payment/GetPaymentInfoForForm/${projectId}`);
    }

    addPayment(paymentForm: PaymentForm) {
        return this.requestPost("api/Payment/AddPayment", paymentForm);
    }

    search(queryString: string) {
        var params = { query: queryString };
        return this.requestGetWithParams("api/Search/Search", params);
    }
}