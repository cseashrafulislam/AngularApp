
// order-list.component.ts
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OrderViewModel } from '../order-form/order.model';


@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  orders: OrderViewModel[] = [];
  apiUrl = 'https://localhost:7157/api/Order'; // API URL for order

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders() {
    this.http.get<OrderViewModel[]>(this.apiUrl).subscribe(
      (data) => {
        this.orders = data;
      },
      (error) => {
        console.error('Error fetching orders', error);
      }
    );
  }

  deleteOrder(id: number) {
    this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
      this.loadOrders(); // Reload orders after deletion
    });
  }
}
