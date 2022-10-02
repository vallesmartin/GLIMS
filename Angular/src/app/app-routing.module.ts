import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { TestPageComponent } from './test-page/test-page.component';
import { LoginComponent } from './security/login.component';
import { LoginDataComponent } from './security/login-data/login-data.component';
import { UsersComponent } from './security/users/users.component';
import { User } from './security/app-user';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EntitiesComponent } from './entities/entities.component';
import { DeliveriesComponent } from './deliveries/deliveries.component';
import { LinesComponent } from './deliveries/lines/lines.component';
import { BillingComponent } from './billing/billing.component';
import { BillingLinesComponent } from './billing/billing-lines/billing-lines.component';
import { ShippingComponent } from './shipping/shipping.component';
import { ShippingLinesComponent } from './shipping/shipping-lines/shipping-lines.component';
import { DebtsComponent } from './debts/debts.component';
import { DebtdetailComponent } from './debts/debtdetail/debtdetail.component';
import { ItemsComponent } from './items/items.component';
import { ItemComponent } from './items/item/item.component';
import { UserComponent } from './security/users/user/user.component';
import { DocumentsComponent } from './documents/documents.component';
import { DocumentsDetailComponent } from './documents/documents-detail/documents-detail.component';
import { EntityDataComponent } from './entities/entity-data/entity-data.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'test-page', component: TestPageComponent},
  { path: 'dashboard', component: DashboardComponent},
  { path: 'entities', component: EntitiesComponent},
  { path: 'entities/entity-data/:id', component: EntityDataComponent},
  { path: 'items', component: ItemsComponent},
  { path: 'items/item/:id', component: ItemComponent},
  { path: 'deliveries', component: DeliveriesComponent},
  { path: 'deliveries/lines/:id', component: LinesComponent },
  { path: 'billing', component: BillingComponent},
  { path: 'billing/billing-lines/:id', component: BillingLinesComponent },
  { path: 'shipping', component: ShippingComponent},
  { path: 'shipping/shipping-lines/:id', component: ShippingLinesComponent },
  { path: 'debts', component: DebtsComponent },
  { path: 'debts/debtdetail/:id', component: DebtdetailComponent },
  { path: 'documents', component: DocumentsComponent },
  { path: 'login', component: LoginComponent},
  { path: 'logindata', component: LoginDataComponent},
  { path: 'users', component: UsersComponent},
  { path: 'users/user/:id/:mode', component: UserComponent},
  { path: 'documents/documents-detail/:id', component: DocumentsDetailComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), HttpClientModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
