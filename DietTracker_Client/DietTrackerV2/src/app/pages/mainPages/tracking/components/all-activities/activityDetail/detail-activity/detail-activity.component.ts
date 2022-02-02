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

  public activity: ActivityDto = {};

  router: Router;
  constructor(private activityService: ActivityService) { }

  ngOnInit() {
    this.activityService.getSingleActivity(this.activityId).subscribe(data => this.activity = data);
  }

  back(){
    console.log(this.activity.id);

    this.router.navigate(['/main-pages/tracking/allActivities']);
  }

}
