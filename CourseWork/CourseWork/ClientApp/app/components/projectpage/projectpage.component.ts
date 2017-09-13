﻿import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';

@Component({
    selector: 'projectpage',
    templateUrl: './projectpage.component.html'
})
export class ProjectPageComponent extends CurrentUserSubscriber {
    project: any = null;

    constructor(private route: ActivatedRoute,
        private title: Title,
        private projectService: ProjectService,
        protected currentUserService: CurrentUserService,
        protected accountService: AccountService) {
        super(currentUserService, accountService);
    }

    ngOnInit() {
        this.route.paramMap.switchMap((params: ParamMap) =>
            this.projectService.getProject(params.get('id'))).subscribe(
            data => {
                this.sortFinancialPurposes(data);
                this.project = data;
                this.title.setTitle(data.name);
            });
    }

    sortFinancialPurposes(data: any) {
        data.financialPurposes.sort((a: any, b: any) => {
            if (a.budget > b.budget) {
                return -1;
            }
            if (a.budget === b.budget) {
                return 0;
            }
            return 1;
        });
    }

    updateRating() {
        this.projectService.changeRating(this.project.id, this.project.rating).subscribe();
    }

    deleteFinancialPurpose(purpose: any) {
        alert("delete");
    }
}