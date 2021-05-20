import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { MainPagesPageRoutingModule } from './main-pages-routing.module';

import { MainPagesPage } from './main-pages.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    MainPagesPageRoutingModule
  ],
  declarations: [MainPagesPage]
})
export class MainPagesPageModule {}
