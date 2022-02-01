
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/api/user.service';
import { Activity } from 'src/app/services/model/activity';

@Component({
  selector: 'app-all-activities',
  templateUrl: './all-activities.component.html',
  styleUrls: ['./all-activities.component.scss'],
})
export class AllActivitiesComponent implements OnInit {
  
  public activities: Activity[] = []
  constructor(private router:Router, private userServies : UserService) { }

  async ngOnInit() {
     this.userServies.apiUserGetAllActivitiesGet().subscribe(data => this.activities = data);
  }


  back(){
    this.router.navigate(['/main-pages/tracking/']);
  }
  nextPage(activity: Activity){
    this.router.navigate(['/main-pages/tracking/allActivities/ActivityDetail' ,activity]);
   }
}
