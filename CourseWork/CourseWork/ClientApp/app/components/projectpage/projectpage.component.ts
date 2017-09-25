import { Component} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ProjectService } from '../../services/project.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { StorageService } from '../../services/storage.service';
import { TimeHelper } from '../../helpers/time.helper';
import { LocalizationService } from "../../services/localization.service";
declare var $: any;

@Component({
    selector: 'projectpage',
    templateUrl: './projectpage.component.html'
})
export class ProjectPageComponent {
    project: any = null;
    keys = ["ONE", "TWO", "THREE", "FOUR", "FIVE", "DESCR", "FINANCIALPURPOSES", "COMMENTS", "NEWS",
        "EDITPROJECT", "EDIT", "TOPROJECTPAGE", "SUBSCRIBE", "UNSUBSCRIBE", "PAYEDAMOUNT", "ENDDATE", "PROJECTSTATUS", "PAYMENTS", "PAY"];
    translations = {}
    timeHelper = new TimeHelper(this.localizationService);

    constructor(public storage: StorageService,
        private route: ActivatedRoute,
        private title: Title,
        private projectService: ProjectService,
        private localizationService: LocalizationService) {
        this.localizationService.getTranslations(this.keys).subscribe((data) => {
            this.translations = data;
        });
    }

    ngOnInit() {
        const request = this.route.paramMap.switchMap((params: ParamMap) =>
            this.projectService.getProject(params.get('id')));
        request.subscribe(data => this.initializeProject(data));
    }

    addNews(news: any) {
        news.time = this.timeHelper.getNowTime();
        this.project.news = [news].concat(this.project.news);
    }

    updateRating() {
        this.projectService.changeRating(this.project.id, this.project.rating).subscribe();
    }

    getNeccessaryAmount() {
        const budgets = this.project.financialPurposes.map((x : any)=> x.budget);
        return Math.max.apply(null, budgets);
    }

    subscribe() {
        this.projectService.subscribe(this.project.id).subscribe(
            data => {
                this.project.isSubscriber = true;
            },
            error => this.project.isSubscriber = false
        );
        
    }

    unsubscribe() {
        this.projectService.unsubscribe(this.project.id).subscribe(
            data => {
                this.project.isSubscriber = false;
            },
            error => this.project.isSubscriber = true
        );
    }

    private initializeProject(data: any) {
        this.project = data;
        this.title.setTitle(data.name);
        if (data.color != null) {
            this.initializeColor(data.color);
        }
    }

    private initializeColor(color: string) {
        $("nav").css("background-color", color);
        $("footer").css("background-color", color);
        $(".tabs .tab a").css("background-color", color);
    }
}