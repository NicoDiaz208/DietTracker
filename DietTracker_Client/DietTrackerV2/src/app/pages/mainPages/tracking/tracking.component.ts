import { Component, OnInit } from '@angular/core';
import { SleepService } from 'src/app/services/api/sleep.service';
import { WaterIntakeService } from 'src/app/services/api/waterIntake.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/api/user.service';
import { ObjectId } from 'src/app/services/model/objectId';
@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrls: ['./tracking.component.scss'],
})
export class TrackingComponent implements OnInit {
  private watercount = 0;
  private sleepcount = 0;
  private usrId : ObjectId;

  constructor(private router: Router,private waterintakeService: WaterIntakeService, private sleepService: SleepService, private userService:UserService) {

  }
  ngOnInit(): void {
    this.userService.apiUserGetSingleUserByUsernameGet("Nico").subscribe(data => this.usrId = data.id)
  }

  navigateAchivement(){
    this.router.navigate(['/main-pages/tracking/achievement']);
  }
  navigate(){
    this.router.navigate(['/main-pages/tracking/addCalories']);
  }
  public plus(){
    this.watercount = this.watercount + 1;
    this.waterintakeService.apiWaterIntakePost({ goWC:3, goWG:4}).subscribe();
    console.log(this.usrId)

  }

  public minus(){
    this.watercount = this.watercount - 1;
    if(this.watercount < 0){
      this.watercount = 0;
    }
    this.waterintakeService.apiWaterIntakePost({ goWC:3, goWG:4}).subscribe();
  }

  public plusS(){
    this.sleepcount = this.sleepcount + 1;
    this.sleepService.apiSleepPost({hoSG:3,hoSC:4}).subscribe();
  }

  public minusS(){
    this.sleepcount = this.sleepcount - 1;

    if(this.sleepcount < 0){
      this.sleepcount = 0;
    }
  }
}
