import { Component, OnInit } from '@angular/core';
import { SleepService } from 'src/app/services/api/sleep.service';
import { WaterIntakeService } from 'src/app/services/api/waterIntake.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/api/user.service';
import { ObjectId } from 'src/app/services/model/objectId';
import { UserDto } from 'src/app/services/model/userDto';
import { WaterIntakeDto } from 'src/app/services/model/waterIntakeDto';
import { SleepDto } from 'src/app/services/model/sleepDto';
import { CalorieIntakeService } from 'src/app/services/api/calorieIntake.service';
import { CalorieIntakeDto } from 'src/app/services/model/calorieIntakeDto';

@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrls: ['./tracking.component.scss'],
})
export class TrackingComponent implements OnInit {
  public user: UserDto = {};
  public waterIntake: WaterIntakeDto = {};
  public sleep: SleepDto = {};
  public calorieIntake: CalorieIntakeDto = {};
  public calorieIntakeId = '';
  public calorienGoal = 0;

  private sleepId = '';
  private waterId = '';
  private watercount = 0;
  private sleepcount = 0;


  constructor(private router: Router,private waterintakeService: WaterIntakeService,
    private sleepService: SleepService, private userService: UserService,
    private calorieIntakeService: CalorieIntakeService ) {

  }
  async ngOnInit(): Promise<void> {
    this.user = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
    this.waterId = await this.userService.apiUserGetWaterIntakeByDateGet(localStorage.getItem('userId'),new Date()).toPromise();
    this.waterIntake = await this.waterintakeService.getSingleWaterIntake(this.waterId).toPromise();
    this.sleepId = await this.userService.apiUserGetSleepByDateGet(localStorage.getItem('userId'), new Date()).toPromise();
    this.sleep = await this.sleepService.getSingleSleep(this.sleepId).toPromise();
    this.calorieIntakeId =  await this.userService.apiUserGetCalorieIntakeByDateGet(localStorage.getItem('userId'), new Date()).toPromise();
    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(this.calorieIntakeId).toPromise();
    this.calorienGoal = await this.userService.apiUserGetSingleBMRGet(localStorage.getItem('userId')).toPromise();
  }

  navigateAchivement(){
    this.router.navigate(['/main-pages/tracking/achievement']);
  }
  navigate(){
    this.router.navigate(['/main-pages/tracking/addCalories', this.calorieIntakeId]);
    //    this.router.navigate(["/main-pages/recipe/generic" ,cat])
  }

  navigateActivity(){
    this.router.navigate(['/main-pages/tracking/trackActivity']);
   
  }

  increaseWater(){
    this.waterIntake.goWC += 1;
  }

  decreaseWater(){
    if(this.waterIntake.goWC > 0){
      this.waterIntake.goWC -= 1;
    }
    else{
      this.waterIntake.goWC = 0;
    }
  }

  increaseSleep(){
    this.sleep.hoSC += 1;
  }

  decreaseSleep(){
    if(this.sleep.hoSC > 0){
      this.sleep.hoSC -= 1;
    }
    this.sleep.hoSC = 0;
  }

  async update(){
    await this.sleepService.apiSleepReplacePost(this.sleep,this.sleepId).toPromise();
    await this.waterintakeService.apiWaterIntakeReplacePost(this.waterIntake, this.waterId).toPromise();
  }
  navigateAllActivitys(){
    this.router.navigate(['/main-pages/tracking/allActivities']);
  }
}
