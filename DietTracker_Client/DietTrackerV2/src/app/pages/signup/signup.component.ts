import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup,FormBuilder,Validators, FormControl } from '@angular/forms';
import { AlertService } from './../../services/alert/alert.service';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {

  public signupForm: FormGroup;
  constructor(
    private router: Router,
    private readonly fb: FormBuilder,
    ) {
    this.signupForm = this.fb.group({
      firstName:['asdf', [Validators.required]],
      lastName:['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required,Validators.minLength(8)]]
    });
    this.signupForm.controls.firstName.valueChanges.subscribe(x => console.log(x));
  }
  ngOnInit() {}



  navigate(){
    this.router.navigate(['/login']);
  }

  public sumbmit(): void{
    const {firstName, lastName,email, password} = this.signupForm.value;
      console.log('scope is ' + firstName + lastName + email+password);
  }
}
