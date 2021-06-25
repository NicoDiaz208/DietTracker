import { Component, OnInit } from '@angular/core';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { UserService } from 'src/app/services/api/user.service';
import { RecipeDto } from 'src/app/services/model/recipeDto';

@Component({
  selector: 'app-fooder',
  templateUrl: './fooder.component.html',
  styleUrls: ['./fooder.component.scss'],
})
export class FooderComponent implements OnInit {

  public recipe: RecipeDto = {};
  public id: String = '';
  public isAdded = false;

  constructor(private recipeService: RecipeService, private userService: UserService) { }

  async ngOnInit() {
    this.id = await this.recipeService.apiRecipeGetRandomGet().toPromise();
    this.recipe = await this.recipeService.getSingleRecipe(this.id.toString()).toPromise();
    this.isAdded = await this.isAlreadyAdded();
  }

  async update(){
    this.id = await this.recipeService.apiRecipeGetRandomGet().toPromise();
    this.recipe = await this.recipeService.getSingleRecipe(this.id.toString()).toPromise();
    this.isAdded = await this.isAlreadyAdded();
    console.log(this.isAdded);

  }

  async add(){
    await this.userService.apiUserAddRecipeIdToUserPost(localStorage.getItem('userId'), this.recipe.id.toString()).toPromise();
    await this.update();
    }

  async isAlreadyAdded(): Promise<boolean>{
    let recipes = await this.userService.apiUserGetAllRecipesGet(localStorage.getItem('userId')).toPromise();
    let res =  recipes.filter(x=> x.name == this.recipe.name);

    if(res.length > 0){
      return true;
    }
    return false;
  }

}
