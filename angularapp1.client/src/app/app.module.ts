import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OderComponent } from './app/Oders/oder/oder.component';
import { OrderComponent } from './app/order/order.component';

@NgModule({
  declarations: [
    AppComponent,
    OderComponent,
    OrderComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
