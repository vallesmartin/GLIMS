import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './security/login.component';
import { LoginDataComponent } from './security/login-data/login-data.component';
import { UsersComponent } from './security/users/users.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EntitiesComponent } from './entities/entities.component';
import { DeliveriesComponent } from './deliveries/deliveries.component';
import { LinesComponent } from './deliveries/lines/lines.component';
import { BillingComponent } from './billing/billing.component';
import { BillingLinesComponent } from './billing/billing-lines/billing-lines.component';
import { ItemsComponent } from './items/items.component';
import { ItemComponent } from './items/item/item.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent},
  { path: 'entities', component: EntitiesComponent},
  { path: 'items', component: ItemsComponent},
  { path: 'items/item/:id', component: ItemComponent},
  { path: 'deliveries', component: DeliveriesComponent},
  { path: 'deliveries/lines/:id', component: LinesComponent },
  { path: 'billing', component: BillingComponent},
  { path: 'billing/billing-lines/:id', component: BillingLinesComponent },
  { path: 'login', component: LoginComponent},
  { path: 'logindata', component: LoginDataComponent},
  { path: 'users', component: UsersComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), HttpClientModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
