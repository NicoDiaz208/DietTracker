import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/api/user.service';
import { DailyProgressDto } from 'src/app/services/model/models';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-all-daily-stats',
  templateUrl: './all-daily-stats.component.html',
  styleUrls: ['./all-daily-stats.component.scss'],
})
export class AllDailyStatsComponent implements OnInit {
  public progresses: Array<DailyProgressDto> = [];
  constructor(private router: Router, private userService: UserService,public datepipe: DatePipe) { }

  async ngOnInit() {
    this.progresses = await this.userService.apiUserGetAllDailyProgressExtendedGet(localStorage.getItem('userId')).toPromise();

  }


  back(){
    this.router.navigate(['/main-pages/tracking/']);
  }
  nextPage(progress: Date){
    const str = this.datepipe.transform(progress, 'yyyy-MM-ddT15:53:58.7Z');

    //this.router.navigate(['/main-pages/recipe/generic',cat]);
    this.router.navigate(['/main-pages/tracking/allActivities/DetailStats',str]);
   }
}
