import { Component, OnInit } from '@angular/core';
import { RecipeService } from 'src/app/services/api/recipe.service';

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

  constructor(private recipeService: RecipeService) {  }

  ngOnInit()
  {
    this.recipeService.apiRecipeGetAllCategoriesGet().subscribe(data=> this.categories = data.map(i=> i.category));
  }

  clickingOnAmel(clicked: string){
    if (this.categories.find(i=> i === clicked))
    {
      this.categories.splice(this.categories.indexOf(clicked),1);
    }
    if(this.currentCategory !== '' && !this.categories.find(i=> i === this.currentCategory)){
      this.categories.push(this.currentCategory);
    }
    this.currentCategory = clicked;
  }

}
