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

  constructor(private recipeService: RecipeService, private userService: UserService) { }

  async ngOnInit() {
    this.recipe = await this.recipeService.apiRecipeGetRandomGet().toPromise();
  }

  async update(){
    this.recipe = await this.recipeService.apiRecipeGetRandomGet().toPromise();
  }

  async add(){
    await this.userService.apiUserAddRecipeIdToUserPost(localStorage.getItem('userId'), this.recipe.id.toString()).toPromise();
  }

}
