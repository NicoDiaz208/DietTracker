import { Component, OnInit } from '@angular/core';
import { AchievementsService } from 'src/app/services/api/achievements.service';
import { UserService } from 'src/app/services/api/user.service';
import { AchievementDto } from 'src/app/services/model/achievementDto';


@Component({
  selector: 'app-achievement',
  templateUrl: './achievement.component.html',
  styleUrls: ['./achievement.component.scss'],
})
export class AchievementComponent implements OnInit {
  public achivements: Array<AchievementDto>;

  constructor( private restService: AchievementsService, private userService:UserService) { }

  async ngOnInit() {
    this.restService.apiAchievementsGet().subscribe(data => {this.achivements = (data	as AchievementDto[])});
  }

}
