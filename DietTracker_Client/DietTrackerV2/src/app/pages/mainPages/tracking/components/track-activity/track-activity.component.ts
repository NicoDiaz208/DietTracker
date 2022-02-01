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

  public user:UserDto = {}
  public activity: ActivityCreationDto = {}
  public activities: ActivityDto[] = []
  private router:Router


  constructor(private userService: UserService, private activityService : ActivityService) { 
    
  }

  async ngOnInit() {
    
    this.activities = await this.activityService.apiActivityGet().toPromise()
  }

  add(){
    this.activity.date = new Date(Date.now())
    console.log(this.activity.date)
    this.activityService.apiActivityPost(this.activity)
  }
  back(){
    this.router.navigate(['/main-pages/tracking/']);
  }

}
