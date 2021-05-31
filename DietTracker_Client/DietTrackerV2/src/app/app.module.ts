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


@NgModule({
  declarations: [
    AppComponent,
    TabBarBottomComponent,
    MainPagesPage,
    FooderComponent,
    ProfileComponent,
    RecipeComponent,
    TabBarBottomComponent
  ],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }, RecipeService,WaterIntakeService,
 {provide: BASE_PATH, useValue:environment.apiBase}
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
