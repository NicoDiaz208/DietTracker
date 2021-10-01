import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { FoodService } from 'src/app/services/api/food.service';
import { Food } from 'src/app/services/model/food';

@Component({
  selector: 'app-modal-food',
  templateUrl: './modal-food.component.html',
  styleUrls: ['./modal-food.component.scss'],
})
export class ModalFoodComponent implements OnInit {
  @Input() selected: {id: string; amount: number}[];
  public ingredientsAll: Food[] = [];
  public ingredientsPresentation: Food[] = [];
  public searchString = '';

  constructor(private foodService: FoodService, private modalController: ModalController) { }

  ngOnInit() {
    this.foodService.apiFoodGet().subscribe(i=> {
      this.ingredientsAll = i;
      this.ingredientsPresentation = i;
    });
  }

  search(){
    this.ingredientsPresentation = this.ingredientsAll.filter(i=> i.name.startsWith(this.searchString));
  }

  select(id: string, amount: string){

    if(amount === ''){
      return;
    }

    if(this.selected.find(i=> i.id === id) !== undefined){
      this.selected.splice(this.selected.indexOf(this.selected.find(i=> i.id === id)),1);
    }
    this.selected.push({id, amount: Number(amount)});
  }

  back(){
    this.modalController.dismiss({
      selected: this.selected
    });
  }
}
