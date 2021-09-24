import { Component, Input, OnInit } from '@angular/core';
import { NavParams } from '@ionic/angular';
import { FoodService } from 'src/app/services/api/food.service';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { Food } from 'src/app/services/model/food';
import { FoodDto } from 'src/app/services/model/foodDto';
import { Recipe } from 'src/app/services/model/recipe';
import { RecipeDto } from 'src/app/services/model/recipeDto';

@Component({
  selector: 'app-modal-recipe-category',
  templateUrl: './modal-recipe-category.component.html',
  styleUrls: ['./modal-recipe-category.component.scss'],
})
export class ModalRecipeCategoryComponent implements OnInit {
  @Input() recipe: RecipeDto;
  public foods: FoodDto[];

  constructor(private foodService: FoodService) { }

  ngOnInit() {
    this.foodService.apiFoodGetListOfFoodPost(this.recipe.foodIds).subscribe(i=> this.foods = i);
  }

}
