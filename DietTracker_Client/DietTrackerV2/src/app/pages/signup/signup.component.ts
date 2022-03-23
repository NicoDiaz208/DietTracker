import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup,FormBuilder,Validators, FormControl } from '@angular/forms';
import { AlertService } from './../../services/alert/alert.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { LoginService } from 'src/app/services/api/login.service';
import { UserService } from 'src/app/services/api/user.service';
import { UserCreationDto } from 'src/app/services/model/userCreationDto';
import { LoginDto } from 'src/app/services/model/loginDto';
import { IonDatetime } from '@ionic/angular';
import { format, parseISO } from 'date-fns';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {

  @ViewChild(IonDatetime, { static: true }) datetime: IonDatetime;


  user = '';
  userCreate: UserCreationDto = {};
  loginCreate: LoginDto = {};

  public signupForm: FormGroup;
  constructor(
    private loginService: LoginService,
    private userService: UserService,
    private router: Router,
    private readonly fb: FormBuilder,
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
      let newDate = new Date(this.signupForm.controls.dateOfBirth.value);
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
      this.userService.apiUserPost(this.userCreate).toPromise();
      this.loginService.apiLoginPost(this.loginCreate).toPromise();
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
    return format(parseISO(value), 'MMM dd yyyy');
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
}
