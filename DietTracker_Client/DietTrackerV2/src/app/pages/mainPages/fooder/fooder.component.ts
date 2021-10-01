import { Component, OnInit } from '@angular/core';
import { FoodService } from 'src/app/services/api/food.service';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { UserService } from 'src/app/services/api/user.service';
import { FoodDto } from 'src/app/services/model/foodDto';
import { IngredientDto } from 'src/app/services/model/ingredientDto';
import { RecipeDto } from 'src/app/services/model/recipeDto';

interface IngredientDetail{
  name: string;
  amount: number;
  unit: string;
};

@Component({
  selector: 'app-fooder',
  templateUrl: './fooder.component.html',
  styleUrls: ['./fooder.component.scss'],
})

export class FooderComponent implements OnInit {
  public recipe: RecipeDto = {};
  public id: string;
  public isAdded = false;
  public foodList: {name: string; amount: number; unit: string}[] = [];

  constructor(private recipeService: RecipeService, private userService: UserService, private foodService: FoodService) { }

  async ngOnInit() {
    await this.update();
  }

  async update(){
    this.id = await this.recipeService.apiRecipeGetRandomGet().toPromise();
    this.recipe = await this.recipeService.getSingleRecipe(this.id.toString()).toPromise();
    this.isAdded = await this.isAlreadyAdded();

    this.foodList = [];
    await this.foodService.apiFoodGetListOfFoodPost(this.recipe.foodIds.map(a=>a.id)).subscribe(i=> i.forEach(e => {
      const f: IngredientDto = this.recipe.foodIds.find(fid=> fid.id === e.id);
      this.foodList.push({name: e.name, amount: f.value, unit: f.unit});
    }));
  }

  async add(){
    await this.userService.apiUserAddRecipeIdToUserPost(localStorage.getItem('userId'), this.recipe.id.toString()).toPromise();
    await this.update();
    }

  async isAlreadyAdded(): Promise<boolean>{
    const recipes = await this.userService.apiUserGetAllRecipesGet(localStorage.getItem('userId')).toPromise();
    const res =  recipes.filter(x=> x.id === this.recipe.id);

    if(res.length > 0){
      return true;
    }
    return false;
  }

  getRecipeFoodAmount(id: string, name: string): IngredientDetail{
    const f: IngredientDto = this.recipe.foodIds.find(i=> i.id === id);
    let k: IngredientDetail;
    k.name = name;
    k.amount = f.value;
    k.unit = f.unit;
    return k;
  }

}
