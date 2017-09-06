import "jquery";
import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
import { TagCloudModule } from 'angular-tag-cloud-module';
import { FormsModule } from '@angular/forms';
 
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
import { ConfirmationModalComponent } from './components/confirmationmodal/confirmationmodal.component';
import { ImageDragAndDropComponent } from './components/imageDragAndDrop/imageDragAndDrop.component';

import { BaseService} from './services/base.service';
import { CurrentUserService } from "./services/currentuser.service";
import { TagService } from "./services/tag.service";
import { AccountService } from "./services/account.service";
import { RoleService } from "./services/role.service";

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'UserProjectsPage', component: UserProjectsPageComponent },
    { path: 'AdminPage', component: AdminPageComponent },
    { path: '**', component: ErrorPageComponent },
];

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        TagCloudModule,
        FormsModule,
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
        ImageDragAndDropComponent
    ],
    providers: [
        BaseService,
        CurrentUserService,
        AccountService,
        TagService,
        RoleService
    ],
    bootstrap: [
        AppComponent
    ]
})

export class AppModule { }