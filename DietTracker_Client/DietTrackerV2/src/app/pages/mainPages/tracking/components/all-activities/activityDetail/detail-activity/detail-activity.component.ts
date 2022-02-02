import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ActivityNames } from 'src/app/services/activityNames';
import { ActivityService } from 'src/app/services/api/activity.service';
import { AcitvityEnum } from 'src/app/services/model/acitvityEnum';
import { Activity } from 'src/app/services/model/activity';
import { ActivityDto } from 'src/app/services/model/activityDto';

@Component({
  selector: 'app-detail-activity',
  templateUrl: './detail-activity.component.html',
  styleUrls: ['./detail-activity.component.scss'],
})
export class DetailActivityComponent implements OnInit {
  @Input() activityId = '';

  public names = ActivityNames.instance.names;
  public activityName = '';
  public activity: ActivityDto = {};

  constructor(private activityService: ActivityService, private router: Router, private route: ActivatedRoute,) {
    this.route.paramMap.subscribe(data => this.activityId = data.get('activityId'));
   }

  async ngOnInit() {
    this.activity = await this.activityService.getSingleActivity(this.activityId).toPromise();
    this.activityName = this.getName(this.activity.name);
  }

  back(){
    console.log(this.activity.id);

    this.router.navigate(['/main-pages/tracking/allActivities']);
  }

  private getName(value: number): string{
    return this.names[value];
  }

}
