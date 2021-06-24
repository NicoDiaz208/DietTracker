import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../../services/model/userDto';
import{UserService} from '../../../services/api/user.service';
import { User } from 'src/app/services/model/user';
import { DailyProgressDto } from 'src/app/services/model/dailyProgressDto';
import { DailyProgress } from 'src/app/classes/daily-progress';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  constructor(private userService: UserService) { 

  }
  public user :UserDto = {};
  public progresses: DailyProgress[];

  async ngOnInit() {
    
    this.user = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
    //this.progresses = await this.userService.apiUserGetAllDailyProgressGet(localStorage.getItem('userId')).toPromise();
  }


}
