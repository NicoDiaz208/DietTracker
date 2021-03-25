import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrls: ['./tracking.component.scss'],
})
export class TrackingComponent implements OnInit {
  private watercount:number = 0;
  private sleepcount: number = 0;
  constructor() {

  }
  ngOnInit(): void {

  }

  public plus(){
    this.watercount = this.watercount + 1;
  }

  public minus(){
    this.watercount = this.watercount - 1;

    if(this.watercount < 0){
      this.watercount = 0;
    }
  }

  public plusS(){
    this.sleepcount = this.sleepcount + 1;
  }

  public minusS(){
    this.sleepcount = this.sleepcount - 1;

    if(this.sleepcount < 0){
      this.sleepcount = 0;
    }
  }
}

