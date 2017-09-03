import { Component, OnInit } from '@angular/core';
import { Response} from '@angular/http';
import { CurrentUserService } from '../../services/currentuser.service';
import { CurrentUser } from '../../viewmodels/currentuser';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
})
export class AppComponent implements OnInit { 
  
    currentUser = new CurrentUser();
     
    constructor(private currentUserService: CurrentUserService) { }
     
    ngOnInit() {
        this.currentUserService.getCurrentUserInfo().subscribe((response: Response) => {
            this.currentUser = response.json();
        });
    }
}