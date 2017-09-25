import "jquery";
//import * as FroalaEditor from "froala-editor/js/froala_editor.pkgd.min";
import "froala-editor/js/froala_editor.pkgd.min.js";

import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule, Http }   from '@angular/http';
import { TagCloudModule } from 'angular-tag-cloud-module';
import { FormsModule } from '@angular/forms';
import { MarkdownModule } from 'angular2-markdown';
import { MaterializeModule } from "angular2-materialize";
import { RatingModule } from "ngx-rating";
import { ColorPickerModule } from 'ngx-color-picker';
import { FroalaEditorModule, FroalaViewModule } from 'angular-froala-wysiwyg';
import { APP_INITIALIZER } from '@angular/core';
import { AppConfig } from './app.config';

import { AppComponent } from './components/app/app.component';
import { HomePageComponent } from './components/homepage/homepage.component';
import { ErrorPageComponent } from './components/errorpage/errorpage.component';
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
import { FinancialPurposesComponent } from './components/financialpurposes/financialpurposes.component';
import { FinancialPurposeModalComponent } from './components/financialpurposemodal/financialpurposemodal.component';
import { ProjectItemComponent } from './components/projectitem/projectitem.component';
import { BaseProjectItemComponent } from './components/baseprojectitem/baseprojectitem.component';
import { NewsFormModalComponent } from './components/newsformmodal/newsformmodal.component';
import { ProjectPageComponent } from './components/projectpage/projectpage.component';
import { ProjectStatusComponent } from './components/projectstatus/projectstatus.component';
import { UserMinInfoComponent } from './components/usermininfo/usermininfo.component';
import { ProjectEditorPageComponent } from './components/projecteditorpage/projecteditorpage.component';
import { NewsComponent } from './components/news/news.component';
import { CommentsComponent } from './components/comments/comments.component';
import { PaymentModalComponent } from './components/paymentmodal/paymentmodal.component';
import { SearcherComponent } from './components/searcher/searcher.component';
import { SearchResultComponent } from './components/searchresult/searchresult.component';
import { UserPageComponent } from './components/userpage/userpage.component';
import { UserCardComponent } from "./components/usercard/usercard.component";
import { AccountEditModalComponent } from "./components/accounteditmodal/accounteditmodal.component";
import { AvatarChangeModalComponent } from "./components/avatarchangemodal/avatarchangemodal.component";
import { UserSubscriptionsComponent } from "./components/usersubscriptions/usersubscriptions.component";
import { LanguageSelectorComponent } from "./components/languageselector/languageselector.component";
import { PaymentComponent } from "./components/payment/payment.component";
import { ProjectProgressComponent } from "./components/projectprogress/projectprogress.component";
import { ColorSelectorComponent } from "./components/colorselector/colorselector.component";
import { AwardComponent } from "./components/award/award.component";
import { ProjectItemCollectionComponent } from "./components/projectitemcollection/projectitemcollection.component";

import { BaseService} from './services/base.service';
import { CurrentUserService } from "./services/currentuser.service"; 
import { TagService } from "./services/tag.service";
import { AccountService } from "./services/account.service";
import { ProjectService } from "./services/project.service";
import { StorageService } from "./services/storage.service";
import { MessageSenderService } from "./services/messagesender.service"
import { MessageSubscriberService } from "./services/messagesubscriber.service";
import { LocalizationService } from "./services/localization.service";
import { ColorService } from "./services/color.service";

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'UserPage', component: UserPageComponent },
    { path: 'AdminPage', component: AdminPageComponent },
    { path: 'NewProjectPage', component: NewProjectPageComponent },
    { path: 'ProjectEditorPage', component: NewProjectPageComponent },
    { path: 'ProjectEditorPage/:id', component: ProjectEditorPageComponent },
    { path: 'UserProjectsPage', component: UserProjectsPageComponent },
    { path: 'ProjectPage/:id', component: ProjectPageComponent },
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
        ColorPickerModule,
        FroalaEditorModule.forRoot(),
        FroalaViewModule.forRoot(),
        RouterModule.forRoot(
            appRoutes,
            { enableTracing: true }
        )
    ],
    declarations: [
        AppComponent,
        HomePageComponent,
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
        FinancialPurposesComponent,
        FinancialPurposeModalComponent,
        BaseProjectItemComponent,
        ProjectItemComponent,
        NewsFormModalComponent,
        ProjectPageComponent,
        ProjectStatusComponent,
        UserMinInfoComponent,
        ProjectEditorPageComponent,
        NewsComponent,
        CommentsComponent,
        PaymentModalComponent,
        NewsFormModalComponent,
        SearcherComponent,
        SearchResultComponent,
        UserPageComponent,
        UserCardComponent,
        AccountEditModalComponent,
        AvatarChangeModalComponent,
        LanguageSelectorComponent,
        UserSubscriptionsComponent,
        PaymentComponent,
        ProjectProgressComponent,
        LanguageSelectorComponent,
        ColorSelectorComponent,
        ProjectProgressComponent,
        AwardComponent,
        ProjectItemCollectionComponent
    ],
    providers: [
        BaseService,
        CurrentUserService,
        AccountService,
        TagService,
        ProjectService,
        StorageService,
		ProjectService,
        MessageSenderService,
        MessageSubscriberService,
        LocalizationService,
        ColorService,
        AppConfig,
        { provide: APP_INITIALIZER, useFactory: (config: AppConfig) => () => config.load(), deps: [AppConfig], multi: true }
    ],
    bootstrap: [
        AppComponent
    ]
})

export class AppModule { }