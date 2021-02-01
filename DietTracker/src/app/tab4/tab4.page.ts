import { Component, OnInit } from '@angular/core';
import { User } from './user';

@Component({
  selector: 'app-tab4',
  templateUrl: './tab4.page.html',
  styleUrls: ['./tab4.page.scss'],
})
export class Tab4Page implements OnInit {
  private user:User;

  constructor() { 
    let tobias: User = new User("Tobias Fraunberger", 19,90,85);
    this.user = tobias;
  }

  ngOnInit() {
  }

}
