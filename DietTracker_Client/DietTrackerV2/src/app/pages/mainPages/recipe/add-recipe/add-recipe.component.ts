import { Component, OnInit } from '@angular/core';
import { FoodService } from 'src/app/services/api/food.service';
import { RecipeService } from 'src/app/services/api/recipe.service';
import { Food } from 'src/app/services/model/food';
import { ModalController, PickerController, PickerOptions, Platform } from '@ionic/angular';
import { ModalFoodComponent } from './modal-food/modal-food.component';
import { RecipeCreationDto } from 'src/app/services/model/recipeCreationDto';
import {Camera, CameraResultType, CameraSource, Photo} from '@capacitor/camera';
import { Directory, Filesystem } from '@capacitor/filesystem';
import { UserService } from 'src/app/services/api/user.service';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/services/api/category.service';
import { IngredientDto } from 'src/app/services/model/ingredientDto';
import { CategoryDto } from 'src/app/services/model/categoryDto';

const IMAGE_DIR = 'stored-images';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss'],
})
export class AddRecipeComponent implements OnInit {

  public categories: CategoryDto[];
  public currentCategories: CategoryDto[] = [];
  public currentName = '';
  public currentPreparation = '';
  public ingredients: Food[];
  public modalPage: any;
  public selected: IngredientDto[] = [];
  public foodNames: string[] = [];
  public currentDifficulty = 0;
  public currentPreparetime = '';
  public currentPicture = '../../../../../assets/Recipes/noimg.jpg';

  public nameError = false;
  public uploadImageError = false;
  public ingrediendError = false;
  public categoryError = false;
  public prepareTimeError = false;
  public preparationError = false;

  constructor(private recipeService: RecipeService,
    private userService: UserService,
    private foodService: FoodService,
    private categoryService: CategoryService,
    private modalController: ModalController,
    private platform: Platform,
    private router: Router,
    private pickerCtrl: PickerController) {  }

  ngOnInit()
  {
    this.categoryService.apiCategoryGetAllGet().subscribe(data => this.categories = data);
    this.foodService.apiFoodGet().subscribe(i=> this.ingredients = i);
  }

  async showDurationPicker(){
    const opts: PickerOptions={
      buttons: [
        {
          text: 'Cancel',
        role: 'cancel'
      },
      {
        text: 'Done'
      }
      ],
      columns:[
        {
          name: 'duration',
          options:[
            {text: '5 Minutes', value: '5 Minutes'},
            {text: '10 Minutes', value: '10 Minutes'},
            {text: '15 Minutes', value: '15 Minutes'},
            {text: '30 Minutes', value: '30 Minutes'},
            {text: '1 Hour', value: '1 Hour'},
            {text: '1 Hour and a Half', value: '1 Hour and a Half'},
            {text: '2 Hours', value: '2 Hours'},
            {text: '3 Hours', value: '3 Hours'},
            {text: '4 Hours', value: '4 Hours'},
            {text: 'more than 5 Hours', value: 'more than 5 Hours'},
          ]
        }
      ]
    };
    const picker = await this.pickerCtrl.create(opts);
    picker.present();
    picker.onDidDismiss().then(async data=>{
      const col = await  picker.getColumn('duration');
      console.log('col: ', col);
      this.currentPreparetime = col.options[col.selectedIndex].text;
    });
  }

  clickCategoryAdd(clicked: CategoryDto){
    this.currentCategories.push(clicked);
    this.categories.splice(this.categories.indexOf(clicked),1);
  }

  clickCategoryRem(clicked: CategoryDto){
    this.currentCategories.splice(this.currentCategories.indexOf(clicked),1);
    this.categories.push(clicked);
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

  validateInput(): boolean{
    this.uploadImageError = false;
    this.ingrediendError = false;
    this.nameError = false;
    this.preparationError = false;
    this.prepareTimeError = false;
    this.categoryError = false;

    let noError = true;

    if(this.currentName === ''){
      this.nameError = true;
      noError = false;
    }
    if(this.currentPreparation.length < 50){
      this.preparationError = true;
      noError = false;
    }
    if(this.currentPreparetime === ''){
      this.prepareTimeError = true;
      noError = false;
    }
    if(this.currentCategories.length < 1){
      this.categoryError = true;
      noError = false;
    }
    if(this.selected.length < 1){
      this.ingrediendError = true;
      noError = false;
    }
    if(this.currentPicture === '../../../../../assets/Recipes/noimg.jpg'){
      this.uploadImageError = true;
      noError = false;
    }

    return noError;
  }

  save(){

    if(!this.validateInput()){
      return;
    }

    const creation: RecipeCreationDto = {
      difficulty: this.currentDifficulty,
      prepareTime: this.currentPreparetime,
      name: this.currentName,
      preparation: this.currentPreparation,
      category: this.currentCategories,
      foodIds: this.selected
    };
    console.log('saved!');

    //this.recipeService.apiRecipePost()
  }

}
