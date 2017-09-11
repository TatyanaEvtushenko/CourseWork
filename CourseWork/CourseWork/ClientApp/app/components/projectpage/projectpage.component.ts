import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
    selector: 'projectpage',
    templateUrl: './projectpage.component.html'
})
export class ProjectPageComponent {
    project: any;
    route: ActivatedRoute;

    constructor(private title: Title, protected projectService: ProjectService) { }

    ngOnInit() {
        this.route.paramMap.switchMap((params: ParamMap) =>
            this.projectService.getProject(params.get('id'))).subscribe(
            data => {
                this.project = data;
                this.title.setTitle(data.name);
            });
    }
}