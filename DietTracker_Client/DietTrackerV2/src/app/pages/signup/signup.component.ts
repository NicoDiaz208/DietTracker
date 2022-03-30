import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup,FormBuilder,Validators, FormControl } from '@angular/forms';
import { AlertService } from './../../services/alert/alert.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { LoginService } from 'src/app/services/api/login.service';
import { UserService } from 'src/app/services/api/user.service';
import { UserCreationDto } from 'src/app/services/model/userCreationDto';
import { LoginDto } from 'src/app/services/model/loginDto';
import { IonDatetime, Platform } from '@ionic/angular';
import * as dateFns from 'date-fns';
import { Camera, CameraResultType, CameraSource, Photo } from '@capacitor/camera';
import { Filesystem, Directory } from '@capacitor/filesystem';
import { RecipeCreationDto } from 'src/app/services';

const IMAGE_DIR = 'stored-images';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})

export class SignupComponent implements OnInit {

  @ViewChild(IonDatetime, { static: true }) datetime: IonDatetime;

  //addProfileImage:
  public uploadImageError = false;
  public currentPicture = '../../../../../assets/Recipes/noimg.jpg';
  public photo: Photo = null;

  user = '';
  userCreate: UserCreationDto = {};
  loginCreate: LoginDto = {};

  public signupForm: FormGroup;
  constructor(
    private loginService: LoginService,
    private userService: UserService,
    private router: Router,
    private readonly fb: FormBuilder,
    private platform: Platform
    ) {
    this.signupForm = this.fb.group({
      username:['', [Validators.required]],
      password: ['', [Validators.required,Validators.minLength(8)]],
      repassword: ['', [Validators.required]],
      gender:['', [Validators.required]],
      dateOfBirth:[''],
      goalWeight:['', [Validators.required, Validators.min(20),Validators.max(300)]],
      height:['', [Validators.required, Validators.min(0), Validators.max(300)]],
      email:['', [Validators.required, Validators.email]],
      phoneNumber:['', [Validators.required,Validators.pattern('^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$')]],
      activityLevel:['', [Validators.required,Validators.min(1),Validators.max(10)]],
      weight:['', [Validators.required, Validators.min(20),Validators.max(300)]]
    });
  }
  ngOnInit() {}



  mainnav(){
    this.router.navigate(['/main-pages/fooder']);
  }
  navigate(){
    this.router.navigate(['/login']);
  }


  async submitForm() {
    if (this.signupForm.get('password').value !== this.signupForm.get('repassword').value) {
      console.log('Password do not Match');
    }
    else{
      this.userCreate.name = this.signupForm.controls.username.value;
      const newDate = new Date(this.signupForm.controls.dateOfBirth.value);
      this.userCreate.dateOfBirth = newDate;
      this.userCreate.gender = this.signupForm.controls.gender.value;
      this.userCreate.goalWeight = this.signupForm.controls.goalWeight.value;
      this.userCreate.height =this.signupForm.controls.height.value;
      this.userCreate.email = this.signupForm.controls.email.value;
      this.userCreate.phoneNumber = this.signupForm.controls.phoneNumber.value;
      this.userCreate.weight = this.signupForm.controls.weight.value;
      this.userCreate.activityLevel = this.signupForm.controls.activityLevel.value;

      this.loginCreate.password = this.signupForm.controls.password.value;
      this.loginCreate.username = this.signupForm.controls.username.value;

      console.log(this.userCreate);
      console.log(this.loginCreate);
      const postUser = await this.userService.apiUserPost(this.userCreate).toPromise();
      this.loginService.apiLoginPost(this.loginCreate).toPromise();

      this.saveImage(this.photo, postUser.id);

      this.navigate();
    }
  }


  confirm() {
    this.datetime.confirm();
  }

  reset() {
    this.datetime.reset();
  }

  formatDate(value: string) {
    return dateFns.format(dateFns.parseISO(value), 'MMM dd yyyy');
  }

  async userExists(){

    this.user = await this.loginService.apiLoginGetSingleLoginGet(this.signupForm.controls.username.value,
      this.signupForm.controls.password.value).toPromise();

      if(this.user == null)
      {
        return false;
      }
      return true;
  }










  //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


  async selectImage(){
    const image = await Camera.getPhoto({
      quality: 90,
      allowEditing: false,
      resultType: CameraResultType.Uri,
      source: CameraSource.Photos
    });

    if(image){
      this.currentPicture = image.webPath;
      this.photo = image;
      console.log('saved: ', image.webPath);
      //this.saveImage(image);
    }
    else{
      console.log('Capture Image did not work');
    }
  }

  async saveImage(photo: Photo, userId: string): Promise<boolean>{
    if(this.photo === null){
      this.userService.apiUserUploadImagePostForm(this.dataURLtoFile(this.currentPicture, 'empty'), userId).toPromise();
    }
    const base64Data = await this.readAsBase64(photo);

    const fileName = new Date().getTime() + '.jpeg';

    const savedFile = await Filesystem.writeFile({
      directory: Directory.Data,
      path: `${IMAGE_DIR}/${fileName}`,
      data: base64Data
    });

    this.userService.apiUserUploadImagePostForm(this.dataURLtoFile(base64Data, fileName), userId).toPromise();

    return true;
  }

  dataURLtoFile(dataurl, filename) {
    const arr = dataurl.split(',');
        const mime = arr[0].match(/:(.*?);/)[1];
        const bstr = atob(arr[1]);
        let n = bstr.length;
        const u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, {type: mime});
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



}
