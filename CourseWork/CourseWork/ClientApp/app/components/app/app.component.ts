import { Component, OnInit } from '@angular/core';
import { Response} from '@angular/http';
import { AppService } from '../../services/app.service';
import { CurrentUser } from '../../viewmodels/currentuser';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent implements OnInit { 
  
    currentUser = new CurrentUser();
     
    constructor(private appService: AppService) { }
     
    ngOnInit() {
        this.appService.getCurrentUserInfo().subscribe((response: Response) => {
            this.currentUser = response.json();
        });
    }
}