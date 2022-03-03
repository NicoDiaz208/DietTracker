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
  username = '';
  password = '';
  user = '';
  loginForm: FormGroup;
  msg = '';

  constructor(
    private loginService: LoginService,
    private readonly fb: FormBuilder,
    private router: Router)
  {
    this.loginForm = this.fb.group({
      username:['', [Validators.required]],
      password: ['', [Validators.required,Validators.minLength(8)]]
    });
  }

  async submitForm() {
    this.user = await this.loginService.apiLoginGetSingleLoginGet(this.username, this.password).toPromise();
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

  handleKeyUp(e){
    if(e.keyCode === 13){
       this.submitForm();
    }
 }
}
