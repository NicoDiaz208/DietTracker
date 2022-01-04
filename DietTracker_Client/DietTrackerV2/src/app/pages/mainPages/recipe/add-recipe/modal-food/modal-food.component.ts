import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { FoodService } from 'src/app/services/api/food.service';
import { Food } from 'src/app/services/model/food';
import { IngredientDto } from 'src/app/services/model/ingredientDto';

@Component({
  selector: 'app-modal-food',
  templateUrl: './modal-food.component.html',
  styleUrls: ['./modal-food.component.scss'],
})
export class ModalFoodComponent implements OnInit {
  @Input() selected: IngredientDto[];
  @Input() names: string[];
  public ingredientsAll: Food[] = [];
  public ingredientsPresentation: Food[] = [];
  public selectedFoods: IngredientDto[] = [];
  public searchString = '';
  public hideItem = false;

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

  select(id: string, amount: string, name: string){
    console.log(amount);

    if(amount === ''){
      return;
    }

    if(amount === '0'){
      this.names.splice(this.names.indexOf(this.names.find(i=> i === name)),1);
      this.selected.splice(this.selected.indexOf(this.selected.find(i=> i.id === id)),1);
      return;
    }

    if(this.selected.indexOf(this.selected.find(i=> i.id === id)) === -1){
      this.selected.push({id, value: Number(amount)});
    }
    else{
      this.selected.find(i=> i.id === id).value = Number(amount);
    }

    if(this.names.indexOf(this.names.find(i=> i === name)) === -1){
      this.names.push(name);
    }

    // if(this.selected.find(i=> i.id === id) !== undefined){
    //   this.selected.splice(this.selected.indexOf(this.selected.find(i=> i.id === id)),1);
    // }

    // this.selected.push({id, value: Number(amount)});
    // this.names.push(name);
  }

  checkValueOnStart(id: string): number{
    let num = 0;

    this.selected.forEach(element=>{
      if(element.id === id){
        num = element.value;
      }
    });
    return num;
  }

  expand(){
    this.hideItem = !this.hideItem;
  }

  back(){
    this.modalController.dismiss({
      selected: this.selected,
      names: this.names
    });
  }
}
