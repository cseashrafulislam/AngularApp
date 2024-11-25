import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';  // Add CommonModule
import { ProductsComponent } from './products/products.component';  // Your ProductsComponent
import { CustomerComponent } from './customer/customer.component';  // Your CustomerComponent


@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    ProductsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule  // Add CommonModule here
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
