import { AfterViewInit, Component, ElementRef, OnInit, QueryList, ViewChildren } from '@angular/core';
import { GestureController, IonCard, Platform } from '@ionic/angular';
import { FoodService } from 'src/app/services/api/food.service';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { UserService } from 'src/app/services/api/user.service';
import { FoodDto } from 'src/app/services/model/foodDto';
import { Ingredient } from 'src/app/services/model/ingredient';
import { RecipeDto } from 'src/app/services/model/recipeDto';

interface IngredientDetail{
  name: string;
  amount: number;
};

@Component({
  selector: 'app-fooder',
  templateUrl: './fooder.component.html',
  styleUrls: ['./fooder.component.scss'],
})

export class FooderComponent implements OnInit, AfterViewInit {

  @ViewChildren(IonCard, {read: ElementRef}) cards: QueryList<ElementRef>;

  public recipe: RecipeDto = {};
  public recipes: RecipeDto[] = [];
  public id: string;
  public isAdded = false;
  public foodList: {name: string; amount: number}[] = [];

  constructor(
    private recipeService: RecipeService,
    private userService: UserService,
    private foodService: FoodService,
    private gestureCtrl: GestureController,
    private plt: Platform) { }

  async ngOnInit() {
    await this.update();
  }

  async update(){
    this.id = await this.recipeService.apiRecipeGetRandomGet().toPromise();
    //this.recipeService.apiRecipeGetRandomWithCountGet(3).subscribe(x=> this.recipes = x);
    this.recipeService.apiRecipeGetRandomWithCountGet(10).toPromise().then(e => this.recipes = e);
    this.recipe = await this.recipeService.getSingleRecipe(this.id.toString()).toPromise();
    this.isAdded = await this.isAlreadyAdded();

    this.foodList = [];
    await this.foodService.apiFoodGetListOfFoodPost(this.recipe.foodIds.map(a=>a.foodId)).subscribe(i=> i.forEach(e => {
      const f: Ingredient = this.recipe.foodIds.find(fid=> fid.foodId === e.id);
      this.foodList.push({name: e.name, amount: f.value});
    }));

    this.ngAfterViewInit();
  }

  ngAfterViewInit(){
    const cardArray = this.cards.toArray();

    console.log(this.cards.length);

    this.useSwipe(cardArray);
  }

  useSwipe(cardArray){
    console.log('length: ' + cardArray.length);
    for(const card of cardArray){
      const gesture = this.gestureCtrl.create({
        el: card.nativeElement,
        gestureName: 'swipe',
        onStart: ev =>{
          if(this.recipes.length < 2){
            this.update();
          }
        },
        onMove: ev=> {
          console.log('ev: '+ ev);

          card.nativeElement.style.transform = `translateX(${ev.deltaX}px) rotate(${ev.deltaX / 10}deg)`;

        },
        onEnd: ev =>{
          card.nativeElement.style.transition = '0.5s ease-out';

          if(ev.deltaX > 150){
            card.nativeElement.style.transform = `translateX(${+this.plt.width() * 2}px) rotate(${ev.deltaX / 2}deg)`;
          } else if(ev.deltaX < -150){
            card.nativeElement.style.transform = `translateX(-${+this.plt.width() * 2}px) rotate(${ev.deltaX / 2}deg)`;
            console.log(this.recipes.pop().name);
          } else{
            card.nativeElement.style.transform = '';
          }
        }
      });

      gesture.enable(true);
    }
  }

  addRecipes(){
    this.recipeService.apiRecipeGetRandomWithCountGet(3).toPromise().then(e => this.recipes = e);
    this.ngAfterViewInit();
  }

  async add(){
    const i = await this.userService.apiUserAddRecipeIdToUserPost(localStorage.getItem('userId'), this.recipe.id.toString()).toPromise();
    await this.update();
  }

  async isAlreadyAdded(): Promise<boolean>{
    const recipes = await this.userService.apiUserGetAllRecipesGet(localStorage.getItem('userId')).toPromise();
    const res =  recipes.filter(x=> x.name === this.recipe.name);

    if(res.length > 0){
      return true;
    }
    return false;
  }

  getRecipeFoodAmount(id: string, name: string): IngredientDetail{
    const f: Ingredient = this.recipe.foodIds.find(i=> i.foodId === id);
    let k: IngredientDetail;
    k.name = name;
    k.amount = f.value;
    return k;
  }

}
