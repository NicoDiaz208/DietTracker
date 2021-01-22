import { Component } from '@angular/core';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page {
  private watercount:number = 0;
  private sleepcount: number = 0;
  constructor() {
    
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

