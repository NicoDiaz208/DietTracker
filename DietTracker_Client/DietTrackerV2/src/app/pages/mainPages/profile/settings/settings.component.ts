import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/api/user.service';
import { UserDto } from 'src/app/services/model/userDto';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit {

  public userDto: UserDto = {};
  public weight = 0;
  public goalWeight = 0;
  public name = '';

  constructor(private userService: UserService) { }

  async ngOnInit() {
    this.userDto = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
    this.weight = this.userDto.weight;
    this.goalWeight = this.userDto.goalWeight;
    this.name = this.userDto.name;
  }

  async updateWeight(){

    this.userDto.weight = this.weight;
    await this.userService.apiUserReplacePost( this.userDto, localStorage.getItem('userId')).toPromise();


  }
  async updateGoalWeight(){

    this.userDto.goalWeight = this.goalWeight;
    await this.userService.apiUserReplacePost(this.userDto, localStorage.getItem('userId')).toPromise();

  }
  async updateName(){

    this.userDto.name = this.name;
   await this.userService.apiUserReplacePost( this.userDto, localStorage.getItem('userId')).toPromise();

  }

}
