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
import { environment } from 'src/environments/environment';
import { UserService } from './services/api/user.service';
import { SleepService } from './services/api/sleep.service';
import { NutritionFactService } from './services/api/nutritionFact.service';
import { FoodService } from './services/api/food.service';
import { DailyProgressService } from './services/api/dailyProgress.service';
import { CalorieIntakeService } from './services/api/calorieIntake.service';
import { AchievementsService } from './services/api/achievements.service';
import { ActivityService } from './services/api/activity.service';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule} from '@angular/forms';
import { SignupComponent } from './pages/signup/signup.component';
import { GenericRecipesComponent } from './pages/mainPages/recipe/generic-recipes/generic-recipes.component';


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
    GenericRecipesComponent
  ],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }, RecipeService,
    WaterIntakeService,
    UserService,
    SleepService,
    NutritionFactService,
    FoodService,
    DailyProgressService,
    CalorieIntakeService,
    AchievementsService,
    ActivityService,
    RecipeService,
 {provide: BASE_PATH, useValue:environment.apiBase}
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
