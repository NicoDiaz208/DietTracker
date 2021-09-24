import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { UserService } from 'src/app/services/api/user.service';
import { CategoryCounter } from 'src/app/services/model/categoryCounter';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss'],
})
export class RecipeComponent implements OnInit {

  public categories: CategoryCounter[];

  constructor(private restService: RecipeService, private userService: UserService,private route: ActivatedRoute, private router: Router) {
   }



   nextPage(cat: string){
    this.router.navigate(["/main-pages/recipe/generic" ,cat]);
   }

   addRecipe(){
     this.router.navigate(["/main-pages/recipe/add-recipe"]);
   }

  ngOnInit() {
    this.restService.apiRecipeGetAllCategoriesGet().subscribe(data=>this.categories = data);
    this.restService.apiRecipeGetAllCategoriesGet();
  }

}
