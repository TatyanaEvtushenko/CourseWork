import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
 
import { AppComponent } from './components/app/app.component';
import { TestComponent } from './components/testcomponent/test.component';
import { TestComponent1 } from './components/testcomponent1/test.component';
 
const appRoutes: Routes = [
    { path: '', component: AppComponent },
    { path: 'test', component: TestComponent },
    { path: 'test1', component: TestComponent1 }
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
        TestComponent1
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }