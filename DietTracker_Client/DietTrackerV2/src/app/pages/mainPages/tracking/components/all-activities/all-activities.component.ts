
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivityNames } from 'src/app/services/activityNames';
import { UserService } from 'src/app/services/api/user.service';
import { Activity } from 'src/app/services/model/activity';
import { UserDto } from 'src/app/services/model/userDto';

@Component({
  selector: 'app-all-activities',
  templateUrl: './all-activities.component.html',
  styleUrls: ['./all-activities.component.scss'],
})
export class AllActivitiesComponent implements OnInit {

  public names: string[] = ActivityNames.instance.names;
  public activities: Activity[] = [];

  constructor(private router: Router, private userService: UserService) { }

  async ngOnInit() {
     this.userService.apiUserGetAllActivitiesGet(localStorage.getItem('userId')).subscribe(data => this.activities = data);
  }

  getName(name: number): string{
    return this.names[name];
  }


  back(){
    this.router.navigate(['/main-pages/tracking/']);
  }
  nextPage(activity: Activity){
    console.log(activity.id);
    const str: string = activity.id as string;
    console.log(str);

    this.router.navigate(['/main-pages/tracking/allActivities/ActivityDetail' ,activity.id.toString()]);
   }
}
