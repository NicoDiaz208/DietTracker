import { Component, Input, OnInit } from '@angular/core';
import { CalorieIntakeService } from 'src/app/services/api/calorieIntake.service';
import { UserService } from 'src/app/services/api/user.service';
import { CalorieIntakeDto } from 'src/app/services/model/calorieIntakeDto';
import { CalorieIntakeCreationDto } from 'src/app/services/model/calorieIntakeCreationDto';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-calories',
  templateUrl: './add-calories.component.html',
  styleUrls: ['./add-calories.component.scss'],
})
export class AddCaloriesComponent implements OnInit {
  @Input() calorieIntakeId = '';
  b: number;
  public calorieIntake: CalorieIntakeDto = {};
  public newCalorieIntake: CalorieIntakeDto = { fatCurrent: 0, proteinCurrent: 0, calorieCurrent: 0, carbohydratesCurrent: 0 };

  constructor(private userService: UserService, private calorieIntakeService: CalorieIntakeService, private route: ActivatedRoute,
    private router: Router) {
    this.route.paramMap.subscribe(data => this.calorieIntakeId = data.get('calorieIntakeId'));

  }

  async ngOnInit() {
    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(this.calorieIntakeId).toPromise();

    console.log(this.calorieIntake.date);
  }

  async update(){
    console.log(this.calorieIntake.calorieCurrent);
    const creation: CalorieIntakeCreationDto = {};

    creation.calorieCurrent = +this.calorieIntake.calorieCurrent + +this.newCalorieIntake.calorieCurrent;
    creation.carbohydratesCurrent = +this.calorieIntake.carbohydratesCurrent + +this.newCalorieIntake.carbohydratesCurrent;
    creation.proteinCurrent = +this.calorieIntake.proteinCurrent + +this.newCalorieIntake.proteinCurrent;
    creation.fatCurrent = +this.calorieIntake.fatCurrent + +this.newCalorieIntake.fatCurrent;
    creation.date = this.calorieIntake.date;
    creation.fatGoal = this.calorieIntake.fatGoal;
    creation.carbohydratesGoal = this.calorieIntake.carbohydratesGoal;
    creation.calorieGoal = this.calorieIntake.calorieGoal;
    creation.proteinGoal = this.calorieIntake.proteinGoal;

    await this.calorieIntakeService.apiCalorieIntakeReplacePost(creation, this.calorieIntake.id).toPromise();

    this.router.navigate(['/main-pages/tracking/']).then(() => {
      window.location.reload();
    });;
  }

  back(){
    this.router.navigate(['/main-pages/tracking/']).then(() => {
      window.location.reload();
    });;
  }

}
