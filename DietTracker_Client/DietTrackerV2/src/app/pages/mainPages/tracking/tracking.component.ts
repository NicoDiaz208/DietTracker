import { Component, OnInit } from '@angular/core';
import { WaterIntakeService } from 'src/app/services/api/waterIntake.service';
@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrls: ['./tracking.component.scss'],
})
export class TrackingComponent implements OnInit {
  private watercount = 0;
  private sleepcount = 0;
  constructor(private waterintakeService: WaterIntakeService) {

  }
  ngOnInit(): void {

  }

  public plus(){
    this.watercount = this.watercount + 1;
    this.waterintakeService.apiWaterIntakePost({ goWC:3, goWG:4}).subscribe();
  }

  public minus(){
    this.watercount = this.watercount - 1;

    if(this.watercount < 0){
      this.watercount = 0;
    }
  }

  public plusS(){
    this.sleepcount = this.sleepcount + 1;
  }

  public minusS(){
    this.sleepcount = this.sleepcount - 1;

    if(this.sleepcount < 0){
      this.sleepcount = 0;
    }
  }
}
