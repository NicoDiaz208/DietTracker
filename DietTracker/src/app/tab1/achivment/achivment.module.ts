import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AchivmentPageRoutingModule } from './achivment-routing.module';

import { AchivmentPage } from './achivment.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AchivmentPageRoutingModule
  ],
  declarations: [AchivmentPage]
})
export class AchivmentPageModule {}
