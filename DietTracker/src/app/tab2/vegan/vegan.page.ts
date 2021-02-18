import { Component, Input, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Recipe } from '../recipe';

@Component({
  selector: 'app-vegan',
  templateUrl: './vegan.page.html',
  styleUrls: ['./vegan.page.scss'],
})
export class VeganPage implements OnInit {

  http : HttpClient;
  allRecipes : Array<Recipe>;
  recipes : Array<Recipe>;
  @Input() category: string = 'Vegan';
  strFilter : string = '';

  constructor(http : HttpClient) {
    this.http = http;
   }

  ngOnInit() {
    try{
      this.http.get('assets/recipes.json').subscribe(data=> {this.recipes = (data as Recipe[]).filter(x=>x.category == this.category); this.recipes = (data as Recipe[]).filter(x=>x.category == this.category)});
    }
    catch{}
  }

  Sort(mode :string){
    switch (mode){
      case 'A-Z':
          this.recipes.sort((a,b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0));
        break;
      case 'Z-A':
          this.recipes.sort((a,b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0));
          this.recipes.reverse();
        break;
    }
  }

  filter(str:String) : boolean{
    return str.includes(this.strFilter);
  }
}
