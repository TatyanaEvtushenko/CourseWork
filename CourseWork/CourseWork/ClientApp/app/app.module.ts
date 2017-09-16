﻿import "jquery";
import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
import { TagCloudModule } from 'angular-tag-cloud-module';
import { FormsModule } from '@angular/forms';
import { MarkdownModule } from 'angular2-markdown';
import { MaterializeModule } from "angular2-materialize";
import { RatingModule } from "ngx-rating";
 
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
import { AdminConfirmationPopupComponent } from './components/adminconfirmationpopup/adminconfirmationpopup.component';
import { ImageLoaderComponent } from './components/imageloader/imageloader.component';
import { FinancialPurposeComponent } from './components/financialpurpose/financialpurpose.component';
import { FinancialPurposeModalComponent } from './components/financialpurposemodal/financialpurposemodal.component';
import { ProjectItemComponent } from './components/projectitem/projectitem.component';
import { BaseProjectItemComponent } from './components/baseprojectitem/baseprojectitem.component';
import { NewsFormModalComponent } from './components/newsformmodal/newsformmodal.component';
import { SearcherComponent } from './components/searcher/searcher.component';
import { SearchResultComponent } from './components/searchresult/searchresult.component';
import { UserPageComponent } from './components/userpage/userpage.component';
import { UserCardComponent } from "./components/usercard/usercard.component";
import { AccountEditModalComponent } from "./components/accounteditmodal/accounteditmodal.component";

import { BaseService} from './services/base.service';
import { CurrentUserService } from "./services/currentuser.service"; 
import { TagService } from "./services/tag.service";
import { AccountService } from "./services/account.service";
import { ProjectService } from "./services/project.service";
import { MessageSenderService } from "./services/messagesender.service";

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'UserPage', component: UserPageComponent },
    { path: 'AdminPage', component: AdminPageComponent },
    { path: 'ProjectEditorPage', component: NewProjectPageComponent },
    { path: 'SearchResult', component: SearchResultComponent },
    { path: '**', component: ErrorPageComponent }
];

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        TagCloudModule,
        FormsModule,
        MaterializeModule,
        MarkdownModule.forRoot(),
        RatingModule,
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
        AdminConfirmationPopupComponent,
        ImageLoaderComponent,
        NewProjectPageComponent,
        MarkdownEditorComponent,
        ErrorTextComponent,
        TagSearcherComponent,
        FloatingButtonComponent,
        FinancialPurposeComponent,
        FinancialPurposeModalComponent,
        BaseProjectItemComponent,
        ProjectItemComponent,
        NewsFormModalComponent,
        SearcherComponent,
        SearchResultComponent,
        UserPageComponent,
        UserCardComponent,
        AccountEditModalComponent
    ],
    providers: [
        BaseService,
        CurrentUserService,
        AccountService,
        TagService,
		ProjectService,
		MessageSenderService
    ],
    bootstrap: [
        AppComponent
    ]
})

export class AppModule { }