import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup,FormBuilder,Validators } from '@angular/forms';
import { AlertService } from './../../services/alert/alert.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { LoginService } from 'src/app/services/api/login.service';
import { LoadingController } from '@ionic/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  user = '';
  loginForm: FormGroup;
  msg = '';

  constructor(
    private loginService: LoginService,
    private readonly fb: FormBuilder,
    public loadingController: LoadingController,
    private router: Router)
  {
    this.loginForm = this.fb.group({
      username:['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  async submitForm() {
    this.user = await this.loginService.apiLoginGetSingleLoginGet(this.loginForm.controls.username.value,
       this.loginForm.controls.password.value).toPromise();
    localStorage.setItem('userId',this.user);     //save Id locally
    this.mainnav();
  }

  ngOnInit(): void {
  }

  navigateToSignup(){
    this.router.navigate(['/signup']);
  }
  mainnav(){
    this.router.navigate(['/main-pages/fooder']);
  }

  handleSubmit(e){
    e.preventDefault();
    alert(this.msg);
  }

  handleKeyUp(){
       this.submitForm();
   }





 async presentLoading() {
  const loading = await this.loadingController.create({
    cssClass: 'my-custom-class',
    message: 'Please wait...',
    duration: 2000
  });
  await loading.present();

  const { role, data } = await loading.onDidDismiss();
  console.log('Loading dismissed!');
}

async presentLoadingWithOptions() {
  const loading = await this.loadingController.create({
    spinner: null,
    duration: 5000,
    message: 'Click the backdrop to dismiss early...',
    translucent: true,
    cssClass: 'custom-class custom-loading',
    backdropDismiss: true
  });
  await loading.present();

  const { role, data } = await loading.onDidDismiss();
  console.log('Loading dismissed with role:', role);
}
}
