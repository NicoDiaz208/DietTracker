import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../../services/model/userDto';
import{UserService} from '../../../services/api/user.service';
import { User } from 'src/app/services/model/user';
import { DailyProgress } from 'src/app/classes/daily-progress';
import { DailyProgressDto } from 'src/app/services/model/dailyProgressDto';
import { DailyProgressService } from 'src/app/services/api/dailyProgress.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  constructor(private userService: UserService) { }
  public user :User;
  public progresses: Array<DailyProgressDto>;

  async ngOnInit() {
    const ciId = await this.userService.apiUserGetCalorieIntakeByDateGet(localStorage.getItem('clientId'), new Date()).toPromise();
    this.userService.getSingleUser(ciId).subscribe((data)=>{this.user=(data as User);});
    this.userService.apiUserGetAllDailyProgressGet(ciId).subscribe((data) =>{this.progresses=(data.reverse() as DailyProgressDto[]);});

  }


}
