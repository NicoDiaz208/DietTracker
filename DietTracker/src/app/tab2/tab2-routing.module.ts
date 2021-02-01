import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Tab2Page } from './tab2.page';

const routes: Routes = [
  {
    path: '',
    component: Tab2Page,
  },
  {
    path: 'vegan',
    loadChildren: () => import('./vegan/vegan.module').then( m => m.VeganPageModule)
  },
  {
    path: 'vegetarian',
    loadChildren: () => import('./vegetarian/vegetarian.module').then( m => m.VegetarianPageModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class Tab2PageRoutingModule {}
