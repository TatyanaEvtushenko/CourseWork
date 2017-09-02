import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
 
import { AppComponent } from './components/app/app.component';
import { TestComponent } from './components/testcomponent/test.component';
import { HomePageComponent } from './components/homepage/homepage.component';
import { LoginNavComponent } from "./components/loginnav/loginnav.component";

import { AppService } from "./services/app.service";

const appRoutes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'test', component: TestComponent },
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
        TestComponent,
        HomePageComponent,
        LoginNavComponent
    ],
    providers:[
        AppService,
    ],
    bootstrap: [ 
        AppComponent 
    ]
})
export class AppModule { }