import { Component, OnInit } from '@angular/core';
import { User } from './classes/user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  private user: User;

  constructor() { }

  ngOnInit() {}

}
