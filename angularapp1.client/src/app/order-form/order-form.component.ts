import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { OrderViewModel } from '../order-form/order.model';  // Assuming you have this model defined

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})
export class OrderFormComponent implements OnInit {
  order: OrderViewModel = new OrderViewModel();
  customers: any[] = [];  // To hold customer data
  products: any[] = [];    // To hold product data
  orders: any[] = []; // List to store orders
  totalOrders: number = 1;   // Total number of orders in the database
  page: number = 1;          // Current page
  pageSize: number = 5;
  totalCount: number = 1;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadCustomers();  // Load customers data
    this.loadProducts();   // Load products data
    this.loadOrders();
  }

  // Load customers data
  loadCustomers(): void {
    this.http.get<any[]>('https://localhost:7157/api/Customer').subscribe((data) => {
      this.customers = data;
    }, (error) => {
      console.error('Error fetching customers:', error);
    });
  }

  // Load products data
  loadProducts(): void {
    this.http.get<any[]>('https://localhost:7157/api/Product').subscribe((data) => {
      this.products = data;
    }, (error) => {
      console.error('Error fetching products:', error);
    });
  }

  // Update the price of the selected product
  updateProductPrice(index: number): void {
    let selectedProductId = this.order.orderDtls[index].productId;

    console.log('Selected Product ID:', selectedProductId); // Debugging

    // Ensure both are of the same type (e.g., both numbers)
    selectedProductId = +selectedProductId; // Coerce string to number

    if (selectedProductId) {
      const selectedProduct = this.products.find(product => product.id === selectedProductId);
      console.log('Selected Product:', selectedProduct); // Debugging

      if (selectedProduct) {
        this.order.orderDtls[index].price = selectedProduct.price;
      } else {
        console.error('Product not found');
      }
    } else {
      this.order.orderDtls[index].price = 0;
    }
  }

  saveOrder(): void {
    if (this.order.id) {
      // Update existing order
      this.http.put(`https://localhost:7157/api/Order/${this.order.id}`, this.order)
        .subscribe(
          response => {
            console.log('Order updated', response);
            this.resetForm(); // Clear the form after saving
            this.loadOrders(); // Reload or update the order list
          },
          error => console.error('Error updating order', error)
        );
    } else {
      // Create new order
      this.http.post('https://localhost:7157/api/Order', this.order)
        .subscribe(
          response => {
            console.log('Order created', response);
            this.resetForm(); // Clear the form after saving
            this.loadOrders(); // Reload or update the order list
          },
          error => console.error('Error creating order', error)
        );
    }
  }

  // Reset the form by clearing the order object
  resetForm(): void {
    this.order = new OrderViewModel(); // This resets the form to its initial state
    this.order.orderDtls = []; // Clear the product details as well
  }

  loadOrders(): void {
    // Ensure you set the pagination parameters correctly
    const params = new HttpParams()
      .set('pageNumber', this.page.toString())  // Make sure to use pageNumber if that's the variable you're tracking
      .set('pageSize', this.pageSize.toString());

    // Define the API URL
    const apiOrderUrl: string = 'https://localhost:7157/api/Order/GetOrdersWithPage';

    // Make the HTTP GET request to fetch paginated orders
    this.http.get<any>(apiOrderUrl, { params }).subscribe(
      (response) => {
        console.log(response);
        this.orders = response.$values;      
      },
      (error) => {
        console.error('Error fetching orders:', error);  // Handle error if any
      }
    );
  }

  //nextPage(): void {
  //  if (this.page * this.pageSize < this.totalOrders) {
  //    this.page++;
  //    this.loadOrders();  // Load the next page
  //  }
  //}
  //prevPage(): void {
  //  if (this.page > 1) {
  //    this.page--;
  //    this.loadOrders();  // Load the previous page
  //  }
  //}

  //// Go to a specific page
  //goToPage(page: number): void {
  //  this.page = page;
  //  this.loadOrders();
  //}


  addProduct(): void {
    this.order.orderDtls.push({
      productId: 0,
      price: 0,
      qty: 0
    });
  }

  removeProduct(index: number): void {
    this.order.orderDtls.splice(index, 1);
  }
}
