import { Component, OnInit } from '@angular/core';
import { FoodService } from 'src/app/services/api/food.service';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { Food } from 'src/app/services/model/food';
import { ModalController } from '@ionic/angular';
import { ModalFoodComponent } from './modal-food/modal-food.component';
import { RecipeCreationDto } from 'src/app/services/model/recipeCreationDto';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss'],
})
export class AddRecipeComponent implements OnInit {

  public categories: string[];
  public currentCategory = '';
  public currentName = '';
  public currentPreparation = '';
  public ingredients: Food[];
  public modalPage: any;
  public selected: {id: string; amount: number}[] = [];
  public currentDifficulty = 0;
  public currentPreparetime: Date;

  constructor(private recipeService: RecipeService, private foodService: FoodService, private modalController: ModalController) {  }

  ngOnInit()
  {
    this.recipeService.apiRecipeGetAllCategoriesGet().subscribe(data=> this.categories = data.map(i=> i.category));
    this.foodService.apiFoodGet().subscribe(i=> this.ingredients = i);
  }

  clickCategory(clicked: string){
    if (this.categories.find(i=> i === clicked))
    {
      this.categories.splice(this.categories.indexOf(clicked),1);
    }
    if(this.currentCategory !== '' && !this.categories.find(i=> i === this.currentCategory)){
      this.categories.push(this.currentCategory);
    }
    this.currentCategory = clicked;
  }

  async presentModal() {
    const modal = await this.modalController.create({
      component: ModalFoodComponent,
      cssClass: 'my-custom-class',
      componentProps: {
        selected: this.selected
      }
    });
    await modal.present();
    const {data} = await modal.onWillDismiss();
    this.selected = data.selected as {id: string; amount: number}[];
  }

  save(){
    const creation: RecipeCreationDto = {
      category: this.currentCategory,
      difficulty: this.currentDifficulty,
      //preparetime is missing
      name: this.currentName,
      preparation: this.currentPreparation
    };

    //this.recipeService.apiRecipePost()
  }

}
