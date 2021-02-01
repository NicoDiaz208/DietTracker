import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VegetarianPage } from './vegetarian.page';

const routes: Routes = [
  {
    path: '',
    component: VegetarianPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VegetarianPageRoutingModule {}
