import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/app/classes/recipe';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { RecipeDto } from 'src/app/services/model/recipeDto';

@Component({
  selector: 'app-generic-recipes',
  templateUrl: './generic-recipes.component.html',
  styleUrls: ['./generic-recipes.component.scss'],
})
export class GenericRecipesComponent implements OnInit {

  allRecipes: Array<Recipe>;
  public recipes: Array<RecipeDto>;
  @Input() category: string = 'Vegan';
  strFilter: string = '';

  constructor(private restService: RecipeService, private route:ActivatedRoute) {
    this.route.paramMap.subscribe(data => this.category = data.get("category"))
   }

  ngOnInit() {
      this.restService.apiRecipeGet().subscribe(data => {this.recipes = (data	as RecipeDto[])});
      /*
      .subscribe(data=> {this.recipes = (data as Recipe[])
        .filter(x=>x.category === this.category);
        this.recipes = (data as Recipe[]).filter(x=>x.category === this.category);
        */

  }

  filter(){
    //NOT Implemented!!!
  }

}
