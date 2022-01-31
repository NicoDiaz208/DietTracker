import { Component, OnInit } from '@angular/core';
import { ActivityService } from 'src/app/services/api/activity.service';
import { UserService } from 'src/app/services/api/user.service';
import { UserDto } from 'src/app/services/model/userDto';


@Component({
  selector: 'app-track-activity',
  templateUrl: './track-activity.component.html',
  styleUrls: ['./track-activity.component.scss'],
})
export class TrackActivityComponent implements OnInit {

  public user:UserDto = {}

  constructor(private userService: UserService, private activityService : ActivityService) { 
    
  }

  ngOnInit() {
    

  }


}
