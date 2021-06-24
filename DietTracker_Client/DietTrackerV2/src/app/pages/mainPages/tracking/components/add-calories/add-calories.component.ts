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
  @Input() calorieIntakeId: string = '';
  b: number;
  public calorieIntake: CalorieIntakeDto = {};
  constructor(private userService: UserService, private calorieIntakeService: CalorieIntakeService, private route: ActivatedRoute,
    private router: Router) {
    this.route.paramMap.subscribe(data => this.calorieIntakeId = data.get("calorieIntakeId"));

  }

  async ngOnInit() {
    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(this.calorieIntakeId).toPromise();

    console.log(this.calorieIntake.date);
  }

  async update(){
    console.log(this.calorieIntake.calorieCurrent);
    let creation : CalorieIntakeCreationDto = {};

    creation.calorieCurrent = this.calorieIntake.calorieCurrent;
    creation.carbohydratesCurrent = this.calorieIntake.carbohydratesCurrent;
    creation.proteinCurrent = this.calorieIntake.proteinCurrent;
    creation.fatCurrent = this.calorieIntake.fatCurrent;
    creation.date = this.calorieIntake.date;
    creation.fatGoal = this.calorieIntake.fatGoal;
    creation.carbohydratesGoal = this.calorieIntake.carbohydratesGoal;
    creation.calorieGoal = this.calorieIntake.calorieGoal;
    creation.proteinGoal = this.calorieIntake.proteinGoal;

    await this.calorieIntakeService.apiCalorieIntakeReplacePost(creation, this.calorieIntake.id).toPromise();

    this.router.navigate(['/main-pages/tracking/'])
  }
}
