import { Component, OnInit } from '@angular/core';
import { FoodService } from 'src/app/services/api/food.service';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { Food } from 'src/app/services/model/food';
import { ModalController, Platform } from '@ionic/angular';
import { ModalFoodComponent } from './modal-food/modal-food.component';
import { RecipeCreationDto } from 'src/app/services/model/recipeCreationDto';
import { DatePipe } from '@angular/common';
import {Camera, CameraResultType, CameraSource, Photo} from '@capacitor/camera';
import { Directory, Filesystem } from '@capacitor/filesystem';
import { UserService } from 'src/app/services/api/user.service';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/services/api/category.service';
import { IngredientDto } from 'src/app/services/model/ingredientDto';

const IMAGE_DIR = 'stored-images';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss'],
})
export class AddRecipeComponent implements OnInit {

  public categories: string[];
  public currentCategory = '+';
  public currentName = '';
  public currentPreparation = '';
  public ingredients: Food[];
  public modalPage: any;
  public selected: IngredientDto[] = [];
  public foodNames: string[] = [];
  public currentDifficulty = 0;
  public currentPreparetime: string;
  public currentPicture = '../../../../../assets/Recipes/noimg.jpg';

  constructor(private recipeService: RecipeService,
    private userService: UserService,
    private foodService: FoodService,
    private categoryService: CategoryService,
    private modalController: ModalController,
    private platform: Platform,
    private router: Router) {  }

  ngOnInit()
  {
    this.categoryService.apiCategoryGetAllGet().subscribe(data => this.categories = data.map(i=> i.name));
    this.foodService.apiFoodGet().subscribe(i=> this.ingredients = i);
  }

  clickCategory(clicked: string){
    if (this.categories.find(i=> i === clicked))
    {
      this.categories.splice(this.categories.indexOf(clicked),1);
    }
    if(this.currentCategory !== '' && !this.categories.find(i=> i === this.currentCategory)){
      this.categories.push(this.currentCategory);
    }
    this.currentCategory = clicked;
  }

  formatDate(date: string): string{
    const dt = new Date(date);
    return dt.getHours() +':'+dt.getMinutes();
   }

  async presentModal() {
    const modal = await this.modalController.create({
      component: ModalFoodComponent,
      cssClass: 'my-custom-class',
      componentProps: {
        selected: this.selected,
        names: this.foodNames
      }
    });
    await modal.present();
    const {data} = await modal.onWillDismiss();

    this.selected = data.selected as IngredientDto[];
    this.foodNames = data.names as string[];

    this.selected.forEach(element => {
      console.log(element);
    });
  }

  async selectImage(){
    const image = await Camera.getPhoto({
      quality: 90,
      allowEditing: false,
      resultType: CameraResultType.Uri,
      source: CameraSource.Photos
    });

    if(image){
      this.currentPicture = image.webPath;
      console.log('saved: ', image.webPath);
      //this.saveImage(image);
    }
    else{
      console.log('Capture Image did not work');
    }
  }

  async saveImage(photo: Photo){
    const base64Data = await this.readAsBase64(photo);

    const fileName = new Date().getTime() + '.jpeg';

    const savedFile = await Filesystem.writeFile({
      directory: Directory.Data,
      path: `${IMAGE_DIR}/${fileName}`,
      data: base64Data
    });

    //https://www.youtube.com/watch?v=fU8uM5oU1wY&ab_channel=SimonGrimm
  }

  async readAsBase64(photo: Photo) {
    // "hybrid" will detect Cordova or Capacitor
    if (this.platform.is('hybrid')) {
      // Read the file into base64 format
      const file = await Filesystem.readFile({
        path: photo.path
      });

      return file.data;
    }
    else {
      // Fetch the photo, read as a blob, then convert to base64 format
      const response = await fetch(photo.webPath);
      const blob = await response.blob();

      return await this.convertBlobToBase64(blob) as string;
    }
  }

  //Helper function
  convertBlobToBase64 = (blob: Blob) => new Promise((resolve, reject) =>{
    const reader = new FileReader();
    reader.onerror = reject;
    reader.onload = () => {
      resolve(reader.result);
    };
    reader.readAsDataURL(blob);
  });

  cancel(){
    this.router.navigate(['/main-pages/recipe/']);
  }

  save(){
    const creation: RecipeCreationDto = {
      difficulty: this.currentDifficulty,
      //preparetime is missing
      //prepareTime = 0,
      name: this.currentName,
      preparation: this.currentPreparation
    };
    console.log('saved!');

    //this.recipeService.apiRecipePost()
  }

}
