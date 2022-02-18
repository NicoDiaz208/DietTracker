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

  constructor(private userService: UserService) { }

  async ngOnInit() {
    this.userDto = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
  }

}
