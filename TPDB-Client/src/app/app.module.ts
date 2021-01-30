import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import {PortsComponent} from './ports/ports.component';
import {ProductsComponent} from './products/products.component';
import {CommoditiesComponent} from './commodities/commodities.component';
import {MainLayoutComponent} from './shared/components/main-layout/main-layout.component';

import {MatToolbarModule} from '@angular/material/toolbar';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatButtonModule} from '@angular/material/button';
import {LoginComponent, LoginDialogComponent} from './login/login.component';
import {MatIconModule} from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import {AUTH_API_URL, RES_API_URL} from './shared/app-injection-tokens';
import {environment} from '../environments/environment';
import {JwtModule} from '@auth0/angular-jwt';
import {LOCAL_TOKEN_KEY} from './shared/services/auth.service';

export function tokenGetter(): string {
  return localStorage.getItem(LOCAL_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    PortsComponent,
    ProductsComponent,
    CommoditiesComponent,
    MainLayoutComponent,
    LoginComponent,
    LoginDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,

    MatToolbarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatGridListModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,

    JwtModule.forRoot({
      config: {tokenGetter,
      allowedDomains: environment.tokenAllowedDomains}
    })
  ],
  providers: [{
    provide: AUTH_API_URL,
    useValue: environment.authApi
  }, {
    provide: RES_API_URL,
    useValue: environment.resApi
  }],
  bootstrap: [AppComponent]
})
export class AppModule {
}
