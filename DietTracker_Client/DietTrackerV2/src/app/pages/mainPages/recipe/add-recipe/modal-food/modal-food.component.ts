import { Component, OnInit } from '@angular/core';
import { FoodService } from 'src/app/services/api/food.service';
import { Food } from 'src/app/services/model/food';

@Component({
  selector: 'app-modal-food',
  templateUrl: './modal-food.component.html',
  styleUrls: ['./modal-food.component.scss'],
})
export class ModalFoodComponent implements OnInit {

  public ingredientsAll: Food[];
  public ingredientsPresentation: Food[];
  public searchString = '';
  public selected: {
    id: string;
    amount: number;
  }[];

  constructor(private foodService: FoodService) { }

  ngOnInit() {
    this.foodService.apiFoodGet().subscribe(i=> {
      this.ingredientsAll = i;
      this.ingredientsPresentation = i;
    });
  }

  search(){
    this.ingredientsPresentation = this.ingredientsAll.filter(i=> i.name.startsWith(this.searchString));
  }

  select(id: string, amount: number){
    this.selected.splice(this.selected.indexOf({id,amount}),1);
    this.selected.push({id,amount});
  }
}
