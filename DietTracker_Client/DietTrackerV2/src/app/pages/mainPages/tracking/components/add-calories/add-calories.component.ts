import { Component, OnInit } from '@angular/core';
import { CalorieIntakeService } from 'src/app/services/api/calorieIntake.service';

@Component({
  selector: 'app-add-calories',
  templateUrl: './add-calories.component.html',
  styleUrls: ['./add-calories.component.scss'],
})
export class AddCaloriesComponent implements OnInit {
  b:number;
  service:CalorieIntakeService;
  constructor() { }

  ngOnInit() {}

  public add(calorien:number){
    this.b = calorien + this.b;
  }
}
