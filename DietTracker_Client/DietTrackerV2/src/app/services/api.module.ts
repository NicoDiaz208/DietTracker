import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { AchievementsService } from './api/achievements.service';
import { ActivityService } from './api/activity.service';
import { CalorieIntakeService } from './api/calorieIntake.service';
import { CategoryService } from './api/category.service';
import { DailyProgressService } from './api/dailyProgress.service';
import { FoodService } from './api/food.service';
import { LoginService } from './api/login.service';
import { RecipeService } from './api/recipe.service';
import { SleepService } from './api/sleep.service';
import { UserService } from './api/user.service';
import { WaterIntakeService } from './api/waterIntake.service';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    AchievementsService,
    ActivityService,
    CalorieIntakeService,
    CategoryService,
    DailyProgressService,
    FoodService,
    LoginService,
    RecipeService,
    SleepService,
    UserService,
    WaterIntakeService ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders<ApiModule> {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}
