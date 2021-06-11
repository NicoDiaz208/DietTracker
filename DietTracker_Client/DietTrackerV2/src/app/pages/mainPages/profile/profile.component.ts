import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../../services/model/userDto';
import{UserService} from '../../../services/api/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  constructor(public userService: UserService) { }
  user?:UserDto;

  ngOnInit() {
    this.userService.getSingleUser('60c20ed3be125b81483888d4').subscribe((data)=>{this.user=data;});
  }


}
