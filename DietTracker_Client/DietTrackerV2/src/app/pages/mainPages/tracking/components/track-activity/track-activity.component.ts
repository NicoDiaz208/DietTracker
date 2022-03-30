import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivityService } from 'src/app/services/api/activity.service';
import { UserService } from 'src/app/services/api/user.service';
import { AcitvityEnum } from 'src/app/services/model/acitvityEnum';
import { Activity } from 'src/app/services/model/activity';
import { ActivityCreationDto } from 'src/app/services/model/activityCreationDto';
import { ActivityDto } from 'src/app/services/model/activityDto';
import { UserDto } from 'src/app/services/model/userDto';
import {ActivityNames } from 'src/app/services/activityNames';
import { CalorieIntakeDto } from 'src/app/services/model/calorieIntakeDto';
import { CalorieIntakeService } from 'src/app/services/api/calorieIntake.service';


@Component({
  selector: 'app-track-activity',
  templateUrl: './track-activity.component.html',
  styleUrls: ['./track-activity.component.scss'],
})
export class TrackActivityComponent implements OnInit {

  public currName: AcitvityEnum = 0;
  public names: string[] = ActivityNames.instance.names;
  public user: UserDto = {};
  public activity: ActivityCreationDto = {};
  public runningActivity: ActivityCreationDto = {};
  public activities: ActivityDto[] = [];
  public errors = '';
  public isError = false;
  public swimmingActivity: ActivityCreationDto = {};
  public walkingActivity: ActivityCreationDto = {};
  public cyclingActivity: ActivityCreationDto = {};
  public calorieIntakeId = '';
  public calorieIntake: CalorieIntakeDto = {};
  private dailyActivities: ActivityDto[] = [];



  // eslint-disable-next-line max-len
  constructor(private userService: UserService, private activityService: ActivityService, private router: Router, private calorieIntakeService: CalorieIntakeService) {
  }

  async ngOnInit() {
    this.user = await this.userService.getSingleUser(localStorage.getItem('userId')).toPromise();
    this.activities = await this.activityService.apiActivityGet().toPromise();
    //this.dailyActivities = await this.userService.apiUserGetAllActivitiesGet().toPromise();
    this.calorieIntakeId =  await this.userService.apiUserGetCalorieIntakeByDateGet(localStorage.getItem('userId'), new Date()).toPromise();
  }

  chooseName(name: number){
    this.currName = name as AcitvityEnum;
  }

  validateInput(): boolean{
    if(this.activity.distance < 0 || this.activity.minutes < 0 ||this.activity.burnedCalories < 0){
      this.errors = 'Only positive numbers';
      return true;
    }

    return false;
  }

  async add(){
    this.isError = this.validateInput();
    if(this.isError){
      return;
    }
    console.log(this.calorieIntake.id);
    this.activity.date = new Date(Date.now());
    this.activity.name = this.currName;

    console.log(this.activity.name);
    const id = await this.activityService.apiActivityPost(this.activity).toPromise();
    await this.userService.apiUserAddActivityToUserPost(this.user.id, id.id).toPromise();

    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(this.calorieIntakeId).toPromise();
    this.calorieIntake.calorieCurrent -= this.activity.burnedCalories;
    await this.calorieIntakeService.apiCalorieIntakeReplacePost(this.calorieIntake, this.calorieIntake.id).toPromise();
    this.back();
  }
  back(){
    this.router.navigate(['/main-pages/tracking/']).then(() => {
      window.location.reload();
    });;
  }


  async addRunning(){
     this.runningActivity.date = new Date(Date.now());
    this.runningActivity.name = 0;
    // eslint-disable-next-line max-len
    this.runningActivity.burnedCalories= await this.userService.apiUserGetCaloriesFromRunningGet(this.user.id, this.runningActivity.minutes).toPromise();

    const id = await this.activityService.apiActivityPost(this.runningActivity).toPromise();
    await this.userService.apiUserAddActivityToUserPost(this.user.id, id.id).toPromise();
    //
    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(this.calorieIntakeId).toPromise();
    this.calorieIntake.calorieCurrent -= this.runningActivity.burnedCalories;
    await this.calorieIntakeService.apiCalorieIntakeReplacePost(this.calorieIntake, this.calorieIntake.id).toPromise();
    this.back();
  }
  async addSwimming(){
    this.swimmingActivity.date = new Date(Date.now());
    this.swimmingActivity.name = 1;

    // eslint-disable-next-line max-len
    this.swimmingActivity.burnedCalories= await this.userService.apiUserGetCaloriesFromSwimmingGet(this.user.id, this.swimmingActivity.minutes).toPromise();
    console.log(this.swimmingActivity.burnedCalories);

    const id = await this.activityService.apiActivityPost(this.swimmingActivity).toPromise();
    await this.userService.apiUserAddActivityToUserPost(this.user.id, id.id).toPromise();
    console.log(id.id);
    console.log(this.calorieIntakeId);

    //
    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(this.calorieIntakeId).toPromise();
    console.log(this.calorieIntake.id);

    this.calorieIntake.calorieCurrent -= this.swimmingActivity.burnedCalories;
    console.log(this.calorieIntake.id.length);
    await this.calorieIntakeService.apiCalorieIntakeReplacePost(this.calorieIntake, this.calorieIntake.id).toPromise();
    this.back();
  }

  async addWalking(){
    this.walkingActivity.date = new Date(Date.now());
    this.walkingActivity.name = 3;

    // eslint-disable-next-line max-len
    this.walkingActivity.burnedCalories= await this.userService.apiUserGetCaloriesFromWalkingGet(this.user.id, this.walkingActivity.minutes).toPromise();
    const id = await this.activityService.apiActivityPost(this.walkingActivity).toPromise();
    await this.userService.apiUserAddActivityToUserPost(this.user.id, id.id).toPromise();
    //
    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(this.calorieIntakeId).toPromise();
    this.calorieIntake.calorieCurrent -= this.walkingActivity.burnedCalories;
    await this.calorieIntakeService.apiCalorieIntakeReplacePost(this.calorieIntake, this.calorieIntake.id).toPromise();
    this.back();
  }

  async addCycling(){
    this.cyclingActivity.date = new Date(Date.now());
    this.cyclingActivity.name = 4;

    // eslint-disable-next-line max-len
    this.cyclingActivity.burnedCalories= await this.userService.apiUserGetCaloriesFromBicyclingGet(this.user.id, this.cyclingActivity.minutes).toPromise();
    const id = await this.activityService.apiActivityPost(this.cyclingActivity).toPromise();
    await this.userService.apiUserAddActivityToUserPost(this.user.id, id.id).toPromise();
    //
    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(this.calorieIntakeId).toPromise();
    this.calorieIntake.calorieCurrent -= this.cyclingActivity.burnedCalories;
    await this.calorieIntakeService.apiCalorieIntakeReplacePost(this.calorieIntake, this.calorieIntake.id).toPromise();
    this.back();
  }

}
