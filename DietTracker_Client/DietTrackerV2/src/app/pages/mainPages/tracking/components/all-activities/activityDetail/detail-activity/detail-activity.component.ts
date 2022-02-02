import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-detail-activity',
  templateUrl: './detail-activity.component.html',
  styleUrls: ['./detail-activity.component.scss'],
})
export class DetailActivityComponent implements OnInit {
  router: Router;
  constructor() { }

  ngOnInit() {}

  back(){
    this.router.navigate(['/main-pages/tracking/allActivities'])
  }

}
