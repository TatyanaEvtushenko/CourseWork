import "jquery";
import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
import { TagCloudModule } from 'angular-tag-cloud-module';
import { FormsModule } from '@angular/forms';
import { MarkdownModule } from 'angular2-markdown';
import { MaterializeModule } from "angular2-materialize";
import { StarRatingModule } from 'angular-star-rating';
 
import { AppComponent } from './components/app/app.component';
import { HomePageComponent } from './components/homepage/homepage.component';
import { ErrorPageComponent } from './components/errorpage/errorpage.component';
import { PageLinksComponent } from "./components/pagelinks/pagelinks.component";
import { UserProjectsPageComponent } from './components/userprojectspage/userprojectspage.component';
import { AdminPageComponent } from './components/adminpage/adminpage.component';
import { RegisterModalComponent } from './components/registermodal/registermodal.component';
import { LoginModalComponent } from './components/loginmodal/loginmodal.component';
import { PreloaderComponent } from './components/preloader/preloader.component';
import { TagCloudComponent } from './components/tagcloud/tagcloud.component';
import { NewProjectPageComponent } from './components/newprojectpage/newprojectpage.component';
import { MarkdownEditorComponent } from './components/markdowneditor/markdowneditor.component';
import { ErrorTextComponent } from './components/errortext/errortext.component';
import { TagSearcherComponent } from './components/tagsearcher/tagsearcher.component';
import { FloatingButtonComponent } from './components/floatingbutton/floatingbutton.component';
import { ConfirmationModalComponent } from './components/confirmationmodal/confirmationmodal.component';
import { ImageLoaderComponent } from './components/imageloader/imageloader.component';
import { FinancialPurposeComponent } from './components/financialpurpose/financialpurpose.component';
import { FinancialPurposeModalComponent } from './components/financialpurposemodal/financialpurposemodal.component';
import { ProjectItemComponent } from './components/projectitem/projectitem.component';
import { NewsFormModalComponent } from './components/newsformmodal/newsformmodal.component';

import { BaseService} from './services/base.service';
import { CurrentUserService } from "./services/currentuser.service"; 
import { TagService } from "./services/tag.service";
import { AccountService } from "./services/account.service";
import { ProjectService } from "./services/project.service";

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'UserProjectsPage', component: UserProjectsPageComponent },
    { path: 'AdminPage', component: AdminPageComponent },
    { path: 'ProjectEditorPage', component: NewProjectPageComponent },
    { path: '**', component: ErrorPageComponent },
];

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        TagCloudModule,
        FormsModule,
        MaterializeModule,
        MarkdownModule.forRoot(),
        StarRatingModule.forRoot(),
        RouterModule.forRoot(
            appRoutes,
            { enableTracing: true }
        )
    ],
    declarations: [
        AppComponent,
        HomePageComponent,
        PageLinksComponent,
        UserProjectsPageComponent,
        AdminPageComponent,
        RegisterModalComponent,
        LoginModalComponent,
        PreloaderComponent,
        TagCloudComponent,
        ErrorPageComponent,
        ConfirmationModalComponent,
        ImageLoaderComponent,
        NewProjectPageComponent,
        MarkdownEditorComponent,
        ErrorTextComponent,
        TagSearcherComponent,
        FloatingButtonComponent,
        FinancialPurposeComponent,
        FinancialPurposeModalComponent,
        ProjectItemComponent,
        NewsFormModalComponent
    ],
    providers: [
        BaseService,
        CurrentUserService,
        AccountService,
        TagService,
        ProjectService
    ],
    bootstrap: [
        AppComponent
    ]
})

export class AppModule { }