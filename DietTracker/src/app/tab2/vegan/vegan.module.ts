import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';;
import { HttpClientModule, HttpClient } from '@angular/common/http';

import { IonicModule } from '@ionic/angular';

import { VeganPageRoutingModule } from './vegan-routing.module';

import { VeganPage } from './vegan.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    VeganPageRoutingModule,
    HttpClientModule
  ],
  declarations: [VeganPage]
})
export class VeganPageModule {}
