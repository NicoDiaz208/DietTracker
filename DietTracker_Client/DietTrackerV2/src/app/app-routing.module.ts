import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { ForgotpassComponent } from './pages/forgotpass/forgotpass.component';
import { LoginComponent } from './pages/login/login.component';
import { FooderComponent } from './pages/mainPages/fooder/fooder.component';
import { MainPagesPage } from './pages/mainPages/main-pages.page';
import { ProfileComponent } from './pages/mainPages/profile/profile.component';
import { GenericRecipesComponent } from './pages/mainPages/recipe/generic-recipes/generic-recipes.component';
import { RecipeComponent } from './pages/mainPages/recipe/recipe.component';
import { AchievementComponent } from './pages/mainPages/tracking/components/achievement/achievement.component';
import { AddCaloriesComponent } from './pages/mainPages/tracking/components/add-calories/add-calories.component';
import { TrackingComponent } from './pages/mainPages/tracking/tracking.component';
import { SignupComponent } from './pages/signup/signup.component';
import { AddRecipeComponent } from './pages/mainPages/recipe/add-recipe/add-recipe.component';
import { TrackActivityComponent } from './pages/mainPages/tracking/components/track-activity/track-activity.component';

const routes: Routes = [
  { path:'', pathMatch: 'full', redirectTo: '/login' },
  { path: 'login', component: LoginComponent},
  { path: 'signup', component: SignupComponent},
  { path: 'forgot-pass', component: ForgotpassComponent},
  { path: 'main-pages', component:MainPagesPage,
    children: [
      { path: 'fooder', component: FooderComponent},
      { path: 'recipe', component: RecipeComponent},
      { path: 'recipe/generic/:category', component: GenericRecipesComponent},
      { path: 'tracking', component: TrackingComponent},
      { path: 'tracking/addCalories/:calorieIntakeId', component: AddCaloriesComponent},
      { path: 'profile', component: ProfileComponent},
      { path: 'tracking/achievement', component: AchievementComponent},
      { path: 'recipe/add-recipe', component: AddRecipeComponent},
      { path: 'tracking/track-activity', component : TrackActivityComponent}
    ]
  },
  { path: '**', redirectTo: '/login'},
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
