import { Component, OnInit } from '@angular/core';
import { Achievment } from 'src/app/classes/achievement';
import { AchievementsService } from 'src/app/services/api/achievements.service';
@Component({
  selector: 'app-achivement',
  templateUrl: './achivement.component.html',
  styleUrls: ['./achivement.component.scss'],
})
export class AchievementComponent implements OnInit {
  achievementlist: Achievment[];
  constructor(public achievementService: AchievementsService) { }

  ngOnInit() {

  }

  add(achievement: Achievment){
    this.achievementService.apiAchievementsPost({now:0,goal:2}).subscribe();
  }

  getByID(id: string): Achievment{
    let result: Achievment;
    this.achievementService.apiAchievementsGet().subscribe();
    return result;
  }
}
