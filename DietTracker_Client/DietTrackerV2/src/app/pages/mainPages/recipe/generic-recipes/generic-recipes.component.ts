import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Recipe } from 'src/app/services/model/recipe';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { UserService } from 'src/app/services/api/user.service';
import { RecipeDto } from 'src/app/services/model/recipeDto';
import { ModalController } from '@ionic/angular';
import { ModalRecipeCategoryComponent } from './modal-recipe-category/modal-recipe-category.component';

@Component({
  selector: 'app-generic-recipes',
  templateUrl: './generic-recipes.component.html',
  styleUrls: ['./generic-recipes.component.scss'],
})
export class GenericRecipesComponent implements OnInit {
  @Input() category = 'Vegan';
  allRecipes: Array<RecipeDto>;
  public recipes: Array<RecipeDto>;
  public strFilter: string;

  constructor(private restService: RecipeService,
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private modalController: ModalController) {
    this.route.paramMap.subscribe(data => this.category = data.get('category'));
   }

  async ngOnInit() {
      //this.restService.apiRecipeGet().subscribe(data => {this.recipes = (data	as RecipeDto[])});
      this.restService.apiRecipeGetAllRecipesByCategoryGet(this.category).subscribe(data =>
        {
          this.recipes = (data as RecipeDto[]);
          this.allRecipes = (data as RecipeDto[]);
        });
      //this.userRecipes = await this.userService.apiUserGetAllRecipesGet(localStorage.getItem('userId')).toPromise();

      /*
      .subscribe(data=> {this.recipes = (data as Recipe[])
        .filter(x=>x.category === this.category);
        this.recipes = (data as Recipe[]).filter(x=>x.category === this.category);
        */

  }

  back(){
    this.router.navigate(['/main-pages/recipe/']);
  }

  filter(searchString: string){
    if(searchString === ''){
      this.recipes = this.allRecipes;
    }

    this.recipes = this.allRecipes.filter(x=> x.name.toLowerCase().startsWith(searchString.toLowerCase()));
  }

  async presentModal(recipe: RecipeDto) {
    const modal = await this.modalController.create({
      component: ModalRecipeCategoryComponent,
      cssClass: 'my-custom-class',
      componentProps: {
        recipe
      }
    });

    return await modal.present();
  }

}
