import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {MainLayoutComponent} from './shared/components/main-layout/main-layout.component';
import {PortsComponent} from './ports/ports.component';
import {ProductsComponent} from './products/products.component';
import {CommoditiesComponent} from './commodities/commodities.component';

const routes: Routes = [
  {
    path: '', component: MainLayoutComponent, children: [
      {path: '', redirectTo: '/ports', pathMatch: 'full'},
      {path: 'ports', component: PortsComponent},
      {path: 'products', component: ProductsComponent},
      {path: 'commodities', component: CommoditiesComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
