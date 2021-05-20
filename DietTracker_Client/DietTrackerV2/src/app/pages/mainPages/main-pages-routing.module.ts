import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FooderComponent } from './fooder/fooder.component';

import { MainPagesPage } from './main-pages.page';
import { ProfileComponent } from './profile/profile.component';
import { RecipeComponent } from './recipe/recipe.component';
import { TrackingComponent } from './tracking/tracking.component';

const routes: Routes = [
  {
    path: 'main-pages', component: MainPagesPage
  },
  { path: 'main-pages/fooder', component:FooderComponent},
  { path: 'main-pages/recipe', component:RecipeComponent},
  { path: 'main-pages/tracking', component: TrackingComponent},
  { path: 'main-pages/profile', component: ProfileComponent}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainPagesPageRoutingModule {}
