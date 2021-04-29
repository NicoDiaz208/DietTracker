import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { FooderComponent } from './pages/mainPages/fooder/fooder.component';
import { ProfileComponent } from './pages/mainPages/profile/profile.component';
import { RecipeComponent } from './pages/mainPages/recipe/recipe.component';
import { TrackingComponent } from './pages/mainPages/tracking/tracking.component';
import { SignupComponent } from './pages/signup/signup.component';

const routes: Routes = [
  { path:'', pathMatch: 'full', redirectTo: '/login' },
  { path: 'login', component: LoginComponent},
  { path: 'signup', component: SignupComponent},
  { path: 'fooder', component: FooderComponent},
  { path: 'profile', component: ProfileComponent},
  { path: 'recipe', component: RecipeComponent},
  { path: 'tracking', component: TrackingComponent},
  { path: '**', redirectTo: '/login'}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
