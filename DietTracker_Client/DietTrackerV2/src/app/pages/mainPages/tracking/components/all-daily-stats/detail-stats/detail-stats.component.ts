
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DailyProgressService } from 'src/app/services/api/dailyProgress.service';
import { DailyProgressDto } from 'src/app/services/model/dailyProgressDto';

@Component({
  selector: 'app-detail-stats',
  templateUrl: './detail-stats.component.html',
  styleUrls: ['./detail-stats.component.scss'],
})
export class DetailStatsComponent implements OnInit {
  @Input() progressId = '';
  public progress: DailyProgressDto = {};
  constructor(private progressService: DailyProgressService, private router: Router, private route: ActivatedRoute,) {
    this.route.paramMap.subscribe(data => this.progressId = data.get('progressId'));
  }

  async ngOnInit() {
    this.progress = await this.progressService.getSingleDailyProgress(this.progressId).toPromise();
  }

  back(){


    this.router.navigate(['/main-pages/tracking/allDailyStats']);
  }

}
