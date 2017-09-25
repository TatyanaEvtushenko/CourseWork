import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from "../../services/account.service";
import { StorageService } from "../../services/storage.service";
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, Router } from "@angular/router";
import { LocalizationService } from "../../services/localization.service";

@Component({
    selector: 'searchresult',
    templateUrl: './searchresult.component.html'
})
export class SearchResultComponent {
    selectedProjecId: string = null;
    projects: any[] = [];
    keys = ["SEARCHRESULTS", "NORESULTSEARCH", "FOUND", "PROJECTS_A"];
    translations = {}
    selectedProjectId: string = null;
    isReady = false;

    constructor(private title: Title, private route: ActivatedRoute, private router: Router,
        protected accountService: AccountService, private projectService: ProjectService,
        private storage: StorageService, private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
            title.setTitle(this.translations['SEARCHRESULTS']);
        });
    }

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            this.projectService.search(params['searchQuery']).subscribe(result => {
                this.projects = result;
                this.isReady = true;
            });
        });
    }
}