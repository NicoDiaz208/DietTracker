import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivityService } from 'src/app/services/api/activity.service';
import { Activity } from 'src/app/services/model/activity';
import { ActivityDto } from 'src/app/services/model/activityDto';

@Component({
  selector: 'app-detail-activity',
  templateUrl: './detail-activity.component.html',
  styleUrls: ['./detail-activity.component.scss'],
})
export class DetailActivityComponent implements OnInit {
  @Input() activityId = '';

  activity: ActivityDto = {};

  router: Router;
  constructor(private activityService: ActivityService) { }

  async ngOnInit() {
    this.activity = await this.activityService.getSingleActivity(this.activityId).toPromise();
  }

  back(){
    this.router.navigate(['/main-pages/tracking/allActivities']);
  }

}
