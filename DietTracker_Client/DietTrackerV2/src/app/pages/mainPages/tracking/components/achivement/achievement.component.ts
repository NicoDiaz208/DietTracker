import { Component, OnInit } from '@angular/core';
import { Achievment } from 'src/app/classes/achievement';
import { AchievementService} from '../../../../../services/achievement.service';
@Component({
  selector: 'app-achivement',
  templateUrl: './achivement.component.html',
  styleUrls: ['./achivement.component.scss'],
})
export class AchievementComponent implements OnInit {
  achievementlist: Achievment[];
  constructor(public achievementService: AchievementService) { }

  ngOnInit() {
    this.achievementService.getAll().subscribe(x=>this.achievementlist= x);
  }

  add(achievement: Achievment){
    this.achievementService.addAchievement(achievement).subscribe();
  }

  getByID(id: string): Achievment{
    let result: Achievment;
    this.achievementService.getAchievementByID(id).subscribe(x=> result = x);
    return result;
  }
}
