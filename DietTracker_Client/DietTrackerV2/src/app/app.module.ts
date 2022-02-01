import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { TabBarBottomComponent } from './pages/tab-bar-bottom/tab-bar-bottom.component';
import { MainPagesPageModule } from './pages/mainPages/main-pages.module';
import { MainPagesPage } from './pages/mainPages/main-pages.page';
import { FooderComponent } from './pages/mainPages/fooder/fooder.component';
import { ProfileComponent } from './pages/mainPages/profile/profile.component';
import { RecipeComponent } from './pages/mainPages/recipe/recipe.component';
import { RecipeService } from './services/api/recipe.service';
import { WaterIntakeService } from './services/api/waterIntake.service';
import { HttpClientModule } from '@angular/common/http';
import { BASE_PATH } from './services/variables';
import { environment } from '../environments/environment';
import { UserService } from './services/api/user.service';
import { SleepService } from './services/api/sleep.service';
import { FoodService } from './services/api/food.service';
import { DailyProgressService } from './services/api/dailyProgress.service';
import { CalorieIntakeService } from './services/api/calorieIntake.service';
import { AchievementsService } from './services/api/achievements.service';
import { ActivityService } from './services/api/activity.service';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule} from '@angular/forms';
import { SignupComponent } from './pages/signup/signup.component';
import { GenericRecipesComponent } from './pages/mainPages/recipe/generic-recipes/generic-recipes.component';
import { TrackingComponent } from './pages/mainPages/tracking/tracking.component';
import { AchievementComponent } from './pages/mainPages/tracking/components/achievement/achievement.component';
import { LoginService } from './services/api/login.service';
import { LoginComponent } from './pages/login/login.component';
import { AddCaloriesComponent } from './pages/mainPages/tracking/components/add-calories/add-calories.component';
import { ForgotpassComponent } from './pages/forgotpass/forgotpass.component';
import { AddRecipeComponent } from './pages/mainPages/recipe/add-recipe/add-recipe.component';
import { ModalFoodComponent } from './pages/mainPages/recipe/add-recipe/modal-food/modal-food.component';
import { ModalRecipeCategoryComponent }
from './pages/mainPages/recipe/generic-recipes/modal-recipe-category/modal-recipe-category.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { CategoryService } from './services/api/category.service';
import { TrackActivityComponent } from './pages/mainPages/tracking/components/track-activity/track-activity.component';
import { AllActivitiesComponent } from './pages/mainPages/tracking/components/all-activities/all-activities.component';


@NgModule({
  declarations: [
    AppComponent,
    TabBarBottomComponent,
    MainPagesPage,
    FooderComponent,
    ProfileComponent,
    RecipeComponent,
    TabBarBottomComponent,
    SignupComponent,
    RecipeComponent,
    GenericRecipesComponent,
    TrackingComponent,
    AchievementComponent,
    LoginComponent,
    AddCaloriesComponent,
    ForgotpassComponent,
    AddRecipeComponent,
    ModalFoodComponent,
    ModalRecipeCategoryComponent,
    TrackActivityComponent,
    AllActivitiesComponent
  ],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      // Register the ServiceWorker as soon as the app is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    })
  ],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }, RecipeService,
    WaterIntakeService,
    UserService,
    SleepService,
    FoodService,
    CategoryService,
    DailyProgressService,
    CalorieIntakeService,
    AchievementsService,
    ActivityService,
    RecipeService,
    LoginService,
 {provide: BASE_PATH, useValue:environment.apiBase}
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
