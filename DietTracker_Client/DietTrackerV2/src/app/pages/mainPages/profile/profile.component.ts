import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../../services/model/userDto';
import{UserService} from '../../../services/api/user.service';
import { User } from 'src/app/services/model/user';
import { DailyProgressDto } from 'src/app/services/model/dailyProgressDto';
import { DailyProgress } from 'src/app/services/model/models';
import { NodeWithI18n } from '@angular/compiler';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  public imageUrl = '../../../../assets/Recipes/TestPerson.jpg';
  public user: UserDto = {};
  public progresses: Array<DailyProgress> = [];
  year: number = new Date().getFullYear();
  public age: number;
  public kcal: Array<number> = [];
  public today: Date = new Date();
  constructor(private userService: UserService) {
  }

  async ngOnInit() {
    this.age = await this.userService.apiUserGetAgeGet(localStorage.getItem('userId')).toPromise();
    this.user = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
    this.progresses = await this.userService.apiUserGetAllDailyProgressGet(localStorage.getItem('userId')).toPromise();
    this.kcal = await this.userService.apiUserGetLastProgressGet(localStorage.getItem('userId')).toPromise();

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
