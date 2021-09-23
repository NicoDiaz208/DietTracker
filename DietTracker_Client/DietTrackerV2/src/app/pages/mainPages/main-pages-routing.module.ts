import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FooderComponent } from './fooder/fooder.component';

import { MainPagesPage } from './main-pages.page';
import { ProfileComponent } from './profile/profile.component';
import { RecipeComponent } from './recipe/recipe.component';
import { TrackingComponent } from './tracking/tracking.component';

const routes: Routes = [
  {
    path: '', component: MainPagesPage,
    children: [
      { path: 'fooder', component:FooderComponent},
      { path: 'recipe', component:RecipeComponent},
      { path: 'tracking', component: TrackingComponent},
      { path: 'profile', component: ProfileComponent}
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainPagesPageRoutingModule {}
