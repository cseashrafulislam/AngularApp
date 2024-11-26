import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';  // <-- Import FormsModule
import { CommonModule } from '@angular/common';  // Add CommonModule
import { ProductsComponent } from './products/products.component';  // Your ProductsComponent
import { CustomerComponent } from './customer/customer.component';
import { OrderFormComponent } from './order-form/order-form.component';
import { OrderListComponent } from './order-list/order-list.component';  // Your CustomerComponent


@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    ProductsComponent,
    OrderFormComponent,
    OrderListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule,  // Add CommonModule here
    FormsModule  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
