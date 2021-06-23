import { Component, OnInit } from '@angular/core';
import { SleepService } from 'src/app/services/api/sleep.service';
import { WaterIntakeService } from 'src/app/services/api/waterIntake.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/api/user.service';
import { ObjectId } from 'src/app/services/model/objectId';
import { UserDto } from 'src/app/services/model/userDto';
@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrls: ['./tracking.component.scss'],
})
export class TrackingComponent implements OnInit {
  private watercount = 0;
  private sleepcount = 0;
  private user: UserDto;

  constructor(private router: Router,private waterintakeService: WaterIntakeService,
    private sleepService: SleepService, private userService: UserService) {

  }
  async ngOnInit(): Promise<void> {
    this.user = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
    console.log(this.user.gender);

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
  }

  tryUsername(){
    console.log(localStorage.getItem('userId'));

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
