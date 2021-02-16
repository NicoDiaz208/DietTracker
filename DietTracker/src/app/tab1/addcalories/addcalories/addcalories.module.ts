import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AddcaloriesPageRoutingModule } from './addcalories-routing.module';

import { AddcaloriesPage } from './addcalories.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AddcaloriesPageRoutingModule
  ],
  declarations: [AddcaloriesPage]
})
export class AddcaloriesPageModule {}
