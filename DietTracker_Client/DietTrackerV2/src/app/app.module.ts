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
    AppRoutingModule
  ],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent],
})
export class AppModule {}
