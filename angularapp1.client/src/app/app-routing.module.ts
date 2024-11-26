import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer/customer.component';
import { ProductsComponent } from './products/products.component';
import { OrderFormComponent } from './order-form/order-form.component';
import { OrderListComponent } from './order-list/order-list.component';

const routes: Routes = [
  { path: 'customer', component: CustomerComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'order/create', component: OrderFormComponent }, // To create order
  { path: 'order/edit/:id', component: OrderFormComponent }, // To edit order - probably same form with different mode

  // List all orders
  { path: 'order/list', component: OrderListComponent },

  // Redirect to order list by default
  { path: '', redirectTo: '/order/list', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
