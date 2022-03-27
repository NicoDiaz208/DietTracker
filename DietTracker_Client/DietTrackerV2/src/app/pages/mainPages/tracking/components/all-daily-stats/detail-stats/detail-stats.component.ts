
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DailyProgressService } from 'src/app/services/api/dailyProgress.service';
import { DailyProgressDto } from 'src/app/services/model/dailyProgressDto';
import {  DailyProgressExtendedDto} from 'src/app/services/model/DailyProgressExtendedDto';

import { UserService } from 'src/app/services/api/user.service';

@Component({
  selector: 'app-detail-stats',
  templateUrl: './detail-stats.component.html',
  styleUrls: ['./detail-stats.component.scss'],
})
export class DetailStatsComponent implements OnInit {
  @Input() progressId = '';
  public progress: DailyProgressExtendedDto = {};
  // eslint-disable-next-line max-len
  constructor(private progressService: DailyProgressService, private router: Router, private route: ActivatedRoute,  private userService: UserService) {
    this.route.paramMap.subscribe(data => this.progressId = data.get('progressId'));
  }

  async ngOnInit() {
    // eslint-disable-next-line max-len
    this.progress = await this.userService.apiUserGetSingleDailyProgressExtendedGet(localStorage.getItem('userId'),this.progressId).toPromise();
  }

  back(){

    this.router.navigate(['/main-pages/tracking/allDailyStats']);
  }

}
