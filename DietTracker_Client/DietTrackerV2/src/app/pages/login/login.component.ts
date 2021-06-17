import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup,FormBuilder,Validators } from '@angular/forms';
import { AlertService } from './../../services/alert/alert.service';
import { AuthService } from 'src/app/services/auth/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {


  loginForm: FormGroup;

  constructor(
    private readonly fb: FormBuilder,
    private router: Router,
    )
  {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required,Validators.minLength(8)]]
    });
  }


  submitForm() {
    console.log(this.loginForm.getRawValue());
  }

  ngOnInit(): void {
  }



  public submit(): void{
    console.log(this.loginForm.valid);
    if(this.loginForm.valid){

      const {username, password} = this.loginForm.value;
       console.log(username + ' ' + password);
    }
    console.log(this.loginForm);
    console.log(this.loginForm);
  }




  navigate(){
    this.router.navigate(['/signup']);
  }
}
