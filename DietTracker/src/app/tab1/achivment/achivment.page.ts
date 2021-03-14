import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Achivements } from './achivement';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-achivment',
  templateUrl: './achivment.page.html',
  styleUrls: ['./achivment.page.scss'],
})

export class AchivmentPage implements OnInit {
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
