import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivityService } from 'src/app/services/api/activity.service';
import { UserService } from 'src/app/services/api/user.service';
import { AcitvityEnum } from 'src/app/services/model/acitvityEnum';
import { Activity } from 'src/app/services/model/activity';
import { ActivityCreationDto } from 'src/app/services/model/activityCreationDto';
import { ActivityDto } from 'src/app/services/model/activityDto';
import { UserDto } from 'src/app/services/model/userDto';


@Component({
  selector: 'app-track-activity',
  templateUrl: './track-activity.component.html',
  styleUrls: ['./track-activity.component.scss'],
})
export class TrackActivityComponent implements OnInit {

  public currName: AcitvityEnum = 0;
  public names: string[] = [ 'Running',
  'Swimming',
  'Hiking',
  'Walking',
  'Cycling'];
  public user: UserDto = {};
  public activity: ActivityCreationDto = {};
  public activities: ActivityDto[] = [];
  private router: Router;
  private dailyActivities: Activity[] = [];



  constructor(private userService: UserService, private activityService: ActivityService) {
  }

  async ngOnInit() {
    this.user = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
    this.activities = await this.activityService.apiActivityGet().toPromise();
    //this.dailyActivities = await this.userService.apiUserGetAllActivitiesGet().toPromise();
  }

  chooseName(name: number){
    this.currName = name as AcitvityEnum;
  }

  async add(){
    this.activity.date = new Date(Date.now());
    this.activity.name = this.currName;
    console.log(this.activity.name);
    const id = await this.activityService.apiActivityPost(this.activity).toPromise();
    await this.userService.apiUserAddActivityToUserPost(this.user.id, id.id).toPromise();
  }
  back(){
    this.router.navigate(['/main-pages/tracking/']);
  }

}
