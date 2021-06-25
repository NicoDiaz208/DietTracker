import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../../services/model/userDto';
import{UserService} from '../../../services/api/user.service';
import { User } from 'src/app/services/model/user';
import { DailyProgressDto } from 'src/app/services/model/dailyProgressDto';
import { DailyProgress } from 'src/app/services/model/models';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  public user: UserDto = {};
  public progresses: Array<DailyProgress> = [];
   year: number = new Date().getFullYear();
  public age: number;
  constructor(private userService: UserService) {

  }

  async ngOnInit() {

    this.user = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
    this.progresses = await this.userService.apiUserGetAllDailyProgressGet(localStorage.getItem('userId')).toPromise();
  }




}
