import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Achivements } from '../../../Entities/achievement';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-achievement',
  templateUrl: './achievement.component.html',
  styleUrls: ['./achievement.component.scss'],
})
export class AchievementComponent implements OnInit {

  @Input() index:Number;
  achivments : Array<Achivements>;
  http : HttpClient;

  constructor(private router:ActivatedRoute ) {

  }
  ngOnInit() {
    try{
      this.http.get('assets/achivments.json').subscribe(data=> {this.achivments = (data as Achivements[])})
    }
    catch{}
  }
}
