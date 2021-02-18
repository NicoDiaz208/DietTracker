import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AddcaloriesPage } from './addcalories.page';

const routes: Routes = [
  {
    path: '',
    component: AddcaloriesPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AddcaloriesPageRoutingModule {}
