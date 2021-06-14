import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeService } from 'src/app/services/api/recipe.service';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss'],
})
export class RecipeComponent implements OnInit {

  public categories : String[];

  constructor(private restService:RecipeService, private route:ActivatedRoute, private router : Router) {
   }



   nextPage(cat : String){
    this.router.navigate(["/main-pages/recipe/generic" ,cat])
   }

  ngOnInit() {
    this.restService.apiRecipeGetAllCategoriesGet().subscribe(data=>this.categories = data);
  }

}
