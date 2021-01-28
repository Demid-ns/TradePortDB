import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import {PortsComponent} from './ports/ports.component';
import {ProductsComponent} from './products/products.component';
import {CommoditiesComponent} from './commodities/commodities.component';
import {LoginFormComponent} from './login-form/login-form.component';
import { MainLayoutComponent } from './shared/components/main-layout/main-layout.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatCardModule} from '@angular/material/card';

@NgModule({
  declarations: [
    AppComponent,
    PortsComponent,
    ProductsComponent,
    CommoditiesComponent,
    LoginFormComponent,
    MainLayoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatToolbarModule,
    MatCardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
