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

const IMAGE_DIR = 'stored-images';

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
  public ingredients: Food[];
  public modalPage: any;
  public selected: {id: string; amount: number}[] = [];
  public currentDifficulty = 0;
  public currentPreparetime: Date;

  constructor(private recipeService: RecipeService,
    private userService: UserService,
    private foodService: FoodService,
    private modalController: ModalController,
    private platform: Platform) {  }

  ngOnInit()
  {
    this.recipeService.apiRecipeGetAllCategoriesGet().subscribe(data=> this.categories = data.map(i=> i.category));
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

  async presentModal() {
    const modal = await this.modalController.create({
      component: ModalFoodComponent,
      cssClass: 'my-custom-class',
      componentProps: {
        selected: this.selected
      }
    });
    await modal.present();
    const {data} = await modal.onWillDismiss();
    this.selected = data.selected as {id: string; amount: number}[];
  }

  async selectImage(){
    const image = await Camera.getPhoto({
      quality: 90,
      allowEditing: false,
      resultType: CameraResultType.Uri,
      source: CameraSource.Photos
    });

    console.log(image);

    if(image){
      this.saveImage(image);
    }
  }

  async saveImage(photo: Photo){
    const base64Data = await this.readAsBase64(photo);
    console.log(base64Data);

    const fileName = new Date().getTime() + '.jpeg';

    const savedFile = await Filesystem.writeFile({
      directory: Directory.Data,
      path: `${IMAGE_DIR}/${fileName}`,
      data: base64Data
    });
    console.log('saved: ', savedFile);

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

  save(){
    const creation: RecipeCreationDto = {
      category: this.currentCategory,
      difficulty: this.currentDifficulty,
      //preparetime is missing
      name: this.currentName,
      preparation: this.currentPreparation
    };

    //this.recipeService.apiRecipePost()
  }

}
