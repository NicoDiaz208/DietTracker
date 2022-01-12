import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup,FormBuilder,Validators, FormControl } from '@angular/forms';
import { AlertService } from './../../services/alert/alert.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { LoginService } from 'src/app/services/api/login.service';
import { UserService } from 'src/app/services/api/user.service';
import { UserCreationDto } from 'src/app/services/model/userCreationDto';
import { LoginDto } from 'src/app/services/model/loginDto';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  user = '';
  username = '';
  password = '';
  repassword = '';
  gender = '';
  dateOfBirth = '';
  goalWeight = 0;
  height = 0;
  email = '';
  phoneNumber = '';
  weight = 0;
  activityLevel =0;

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
      repassword: ['', [Validators.required,Validators.minLength(8)]],
      gender:['', [Validators.required]],
      dateOfBirth:['', [Validators.required]],
      goalWeight:['', [Validators.required]],
      height:['', [Validators.required]],
      email:['', [Validators.required]],
      phoneNumber:['', [Validators.required]],
      activityLevel:['', [Validators.required]],
      weight:['', [Validators.required]]
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
      this.userCreate.name = this.username;
      let newDate = new Date(this.dateOfBirth);
      this.userCreate.dateOfBirth = newDate;
      this.userCreate.gender = this.gender;
      this.userCreate.goalWeight = this.goalWeight;
      this.userCreate.height = this.height;
      this.userCreate.email = this.email;
      this.userCreate.phoneNumber = this.phoneNumber;
      this.userCreate.weight = this.weight;
      this.userCreate.activityLevel = this.activityLevel;

      console.log(this.password);

      this.loginCreate.password = this.password;
      this.loginCreate.username = this.username;

      console.log(this.userCreate);
      console.log(this.loginCreate);
      this.userService.apiUserPost(this.userCreate).toPromise();
      this.loginService.apiLoginPost(this.loginCreate).toPromise();
      this.navigate();
    }
  }
}
