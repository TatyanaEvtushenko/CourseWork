import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
 
import { AppComponent } from './components/app/app.component';
import { HomePageComponent } from './components/homepage/homepage.component';
import { LoginNavComponent } from "./components/loginnav/loginnav.component";
import { UserProjectsComponent } from './components/userprojects/userprojects.component';
import { AdminPageComponent } from './components/adminpage/adminpage.component';

import { AppService } from "./services/app.service";

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'UserProjects', component: UserProjectsComponent },
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
        LoginNavComponent,
        UserProjectsComponent,
        AdminPageComponent,
    ],
    providers:[
        AppService,
    ],
    bootstrap: [ 
        AppComponent 
    ]
})
export class AppModule { }