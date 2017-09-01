import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
 
import { AppComponent } from './components/app/app.component';
import { TestComponent } from './components/testcomponent/test.component';
import { TagCloudComponent } from "./components/tagcloud/tagcloud.component";

const appRoutes: Routes = [
    { path: "", component: AppComponent },
    { path: "test", component: TestComponent },
    { path: "tagcloud", component: TagCloudComponent }
];

@NgModule({
    imports: [
        BrowserModule,
        RouterModule.forRoot(
            appRoutes,
            { enableTracing: true }
        )
    ],
    declarations: [
        AppComponent,
        TestComponent,
        TagCloudComponent
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }