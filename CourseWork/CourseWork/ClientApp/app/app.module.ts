import "jquery";
import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
 
import { AppComponent } from './components/app/app.component';
import { HomePageComponent } from './components/homepage/homepage.component';
import { PageLinksComponent } from "./components/pagelinks/pagelinks.component";
import { LoginNavComponent } from "./components/loginnav/loginnav.component";
import { UserProjectsPageComponent } from './components/userprojectspage/userprojectspage.component';
import { AdminPageComponent } from './components/adminpage/adminpage.component';
import { RegisterModalComponent } from './components/registermodal/registermodal.component';
import { LoginModalComponent } from './components/loginmodal/loginmodal.component';
import { PreloaderComponent } from './components/preloader/preloader.component';


import { AppService } from "./services/app.service";

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'UserProjectsPage', component: UserProjectsPageComponent },
    { path: 'AdminPage', component: AdminPageComponent },
];

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        RouterModule.forRoot(
            appRoutes,
            { enableTracing: true }
        )
    ],
    declarations: [
        AppComponent,
        HomePageComponent,
        PageLinksComponent,
        LoginNavComponent,
        UserProjectsPageComponent,
        AdminPageComponent,
        RegisterModalComponent,
        LoginModalComponent,
        PreloaderComponent
    ],
    providers:[
        AppService,
    ],
    bootstrap: [ 
        AppComponent 
    ]
})
export class AppModule { }