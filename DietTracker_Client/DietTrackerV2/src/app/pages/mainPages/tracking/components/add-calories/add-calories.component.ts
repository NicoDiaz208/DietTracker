import { Component, OnInit } from '@angular/core';
import { CalorieIntakeService } from 'src/app/services/api/calorieIntake.service';
import { UserService } from 'src/app/services/api/user.service';
import { CalorieIntakeDto } from 'src/app/services/model/calorieIntakeDto';

@Component({
  selector: 'app-add-calories',
  templateUrl: './add-calories.component.html',
  styleUrls: ['./add-calories.component.scss'],
})
export class AddCaloriesComponent implements OnInit {
  b: number;
  calorieIntake: CalorieIntakeDto;
  constructor(private userService: UserService, private calorieIntakeService: CalorieIntakeService) { }

  async ngOnInit() {
    const ciId = await this.userService.apiUserGetCalorieIntakeByDateGet(localStorage.getItem('clientId'), new Date()).toPromise();
    this.calorieIntake = await this.calorieIntakeService.getSingleCalorieIntake(ciId).toPromise();
  }

  public add(calorien: number){
    this.b = calorien + this.b;
  }
}
