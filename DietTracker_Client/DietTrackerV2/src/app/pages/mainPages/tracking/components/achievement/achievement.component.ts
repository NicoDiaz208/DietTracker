import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AchievementsService } from 'src/app/services/api/achievements.service';
import { UserService } from 'src/app/services/api/user.service';
import { Achievement } from 'src/app/services/model/achievement';
import { AchievementDto } from 'src/app/services/model/achievementDto';


@Component({
  selector: 'app-achievement',
  templateUrl: './achievement.component.html',
  styleUrls: ['./achievement.component.scss'],
})
export class AchievementComponent implements OnInit {
  public achivements: Array<Achievement>;

  constructor( private restService: AchievementsService, private userService: UserService, private router: Router) { }

  async ngOnInit() {
    this.achivements = await this.userService.apiUserGetAllAchievementsGet(localStorage.getItem('userId')).toPromise();
  }
  back(){
    this.router.navigate(['/main-pages/tracking/']);
  }

}
