import { Component, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUserSubscriber } from '../currentuser.subscriber';
import { ProjectService } from '../../services/project.service';

@Component({
    selector: 'userprojectspage',
    templateUrl: './userprojectspage.component.html'
})
export class UserProjectsPageComponent extends CurrentUserSubscriber {
    @Input() projects: any[] = [];
    selectedProjectId: string = null;

	constructor(private title: Title, protected currentUserService: CurrentUserService, protected accountService: AccountService, private projectService: ProjectService) {
        super(currentUserService, accountService);
        //title.setTitle("My projects");
    }

    openNewsModal(event: any) {
        this.selectedProjectId = event;
    }
}