import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import {NewProjectForm} from "../viewmodels/newprojectform";

@Injectable()
export class ProjectService extends BaseService {

    addProject(projectForm: NewProjectForm) {
        console.log(projectForm);
        return this.requestGet("api/Project/AddProject");
    }
}