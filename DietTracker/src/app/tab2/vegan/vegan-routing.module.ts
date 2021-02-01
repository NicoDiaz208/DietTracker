import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VeganPage } from './vegan.page';
import { Tab2Page } from '../tab2.page';

const routes: Routes = [
  { path: '', component: VeganPage },
  { path: 'tab2', component: Tab2Page }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VeganPageRoutingModule {}
