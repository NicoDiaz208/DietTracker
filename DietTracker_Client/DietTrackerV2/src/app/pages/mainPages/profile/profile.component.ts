import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../../services/model/userDto';
import{UserService} from '../../../services/api/user.service';
import { User } from 'src/app/services/model/user';
import { DailyProgressDto } from 'src/app/services/model/dailyProgressDto';

import { NodeWithI18n } from '@angular/compiler';
import { tr } from 'date-fns/locale';
import { DatePipe } from '@angular/common';
import { DailyProgressService } from 'src/app/services';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  public imageUrl = '../../../../assets/Recipes/TestPerson.jpg';
  public user: UserDto = {};
  public profileAvailable = false;

  public progress: DailyProgressDto = {};

  year: number = new Date().getFullYear();
  public age: number;
  public kcal: Array<number> = [];
  public today: Date = new Date();
  constructor(private userService: UserService, private dailyProgressService: DailyProgressService) {
  }

  async ngOnInit() {
    this.age = await this.userService.apiUserGetAgeGet(localStorage.getItem('userId')).toPromise();
    await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise().then(x=> {
      this.user = x;
      this.profileAvailable = true;
    });

    this.kcal = await this.userService.apiUserGetLastProgressGet(localStorage.getItem('userId')).toPromise();

    const progress = await this.userService.apiUserCalculateDailyProgressPost(this.user.id, new Date()).toPromise();
    this.progress = await this.dailyProgressService.getSingleDailyProgress(progress).toPromise();
    console.log(this.progress.percentage);
  }


  getImage(){
    return 'http://diettrackerapi.azurewebsites.net/api/User/images/user/'+this.user.id;
  }

  days(i: number){
    const date = new Date();
    date.setDate(this.today.getDate() -i);
    return date;
  }




}
