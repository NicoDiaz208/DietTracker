import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { FooderComponent } from './fooder/fooder.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RecipeComponent } from './recipe/recipe.component';
import { RegisterComponent } from './register/register.component';
import { AchievementComponent } from './tracking/achievement/achievement.component';
import { AddCaloriesComponent } from './tracking/add-calories/add-calories.component';
import { TrackingComponent } from './tracking/tracking.component';

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then( m => m.HomePageModule)
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'registration',
    component: RegisterComponent
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent
  },
  {
    path: 'profile',
    component: ProfileComponent
  },
  {
    path: 'fooder',
    component: FooderComponent
  },
  {
    path: 'recipe',
    component: RecipeComponent
  },
  {
    path: 'tracking',
    component: TrackingComponent
  },
  {
    path: 'addcalories',
    component: AddCaloriesComponent
  },
  {
    path: 'achievement',
    component: AchievementComponent
  },

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
