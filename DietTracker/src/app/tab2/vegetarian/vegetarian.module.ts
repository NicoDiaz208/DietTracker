import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { VegetarianPageRoutingModule } from './vegetarian-routing.module';

import { VegetarianPage } from './vegetarian.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    VegetarianPageRoutingModule
  ],
  declarations: [VegetarianPage]
})
export class VegetarianPageModule {}
