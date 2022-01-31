import { Component, OnInit } from '@angular/core';
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


  constructor(private userService: UserService, private activityService : ActivityService) { 
    
  }

  async ngOnInit() {
    
    this.activities = await this.activityService.apiActivityGet().toPromise()
  }

  add(){
    

     this.activityService.apiActivityPost(this.activity)
  }


}
