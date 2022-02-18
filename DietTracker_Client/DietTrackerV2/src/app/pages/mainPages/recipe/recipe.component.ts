import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/services/api/category.service';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { UserService } from 'src/app/services/api/user.service';
import { CategoryDto } from 'src/app/services/model/categoryDto';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss'],
})
export class RecipeComponent implements OnInit {

  public categories: CategoryDto[];
  public currentCategories: CategoryDto[];

  constructor(private recipeService: RecipeService, private categoryService: CategoryService,
    private route: ActivatedRoute, private router: Router) {
   }

   filterCategories(searchString: string){
     if(searchString === ''){
        this.currentCategories = this.categories;
     }
     this.currentCategories = this.categories.filter(x=> x.name.toLowerCase().startsWith(searchString.toLowerCase()));
   }

   nextPage(cat: string){
    this.router.navigate(['/main-pages/recipe/generic',cat]);
   }

   addRecipe(){
     this.router.navigate(['/main-pages/recipe/add-recipe']);
   }

  ngOnInit() {
    this.categoryService.apiCategoryGetAllGet().subscribe(data=>{
      this.categories = data;
      this.currentCategories = data;
    });
    //this.recipeService.apiRecipeGetAllCategoriesGet();
  }



}
