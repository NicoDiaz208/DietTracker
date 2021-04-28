import { Component, OnInit } from '@angular/core';
import { User } from './user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  public user:User;
  constructor() { }

  ngOnInit() {}

}
