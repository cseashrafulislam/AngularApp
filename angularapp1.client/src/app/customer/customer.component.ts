import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from './Models/customer';  // Adjust path if needed
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  customers: Customer[] = [];
  customerForm: FormGroup;
  isEditMode: boolean = false;
  selectedCustomerId: number | null = null;
  apiUrl: string = 'https://localhost:7157/api/Customer';

  constructor(private http: HttpClient, private fb: FormBuilder) {
    // Adjust form controls to match the backend model (CustomerViewModel)
    this.customerForm = this.fb.group({
      customerName: ['', Validators.required], // CustomerName instead of name
      phoneNo: ['', Validators.required],       // PhoneNo instead of phone
      address: ['', Validators.required]        // Address instead of address
    });
  }

  ngOnInit(): void {
    this.getCustomers();
  }

  // Fetch all customers
  getCustomers(): void {
    this.http.get<Customer[]>(this.apiUrl).subscribe(
      (data) => {
        console.log('Fetched Customers:', data);  // Log the response to check the data
        this.customers = data;
      },
      (error) => {
        console.error('Error fetching customers:', error);
      }
    );

  }

  // Open the form for adding a customer
  openAddCustomerForm(): void {
    this.isEditMode = false;
    this.customerForm.reset();
    this.selectedCustomerId = null;
  }

  // Open the form for editing a customer
  openEditCustomerForm(customer: Customer): void {
    this.isEditMode = true;
    this.selectedCustomerId = customer.id;
    // Populate the form with customer data
    this.customerForm.patchValue({
      id: customer.id,
      customerName: customer.customerName, // Map to CustomerName
      phoneNo: customer.phoneNo,     // Map to PhoneNo
      address: customer.address    // Map to Address
    });
  }

  // Submit the form for adding or editing a customer
  onSubmit(): void {
    if (this.customerForm.invalid) {
      return; // If form is invalid, prevent submission
    }

    const customerData: Customer = this.customerForm.value;
    

    if (this.isEditMode && this.selectedCustomerId !== null) {
      customerData.id = this.selectedCustomerId;
      this.updateCustomer(this.selectedCustomerId, customerData);
    } else {
      this.createCustomer(customerData);
    }
  }

  // Create a new customer
  createCustomer(customer: Customer): void {
    this.http.post<Customer>(this.apiUrl, customer).subscribe(
      (response) => {
        alert('Customer added successfully!');
        this.getCustomers();
        this.customerForm.reset();
      },
      (error) => {
        console.error('Error adding customer:', error);
        alert('Failed to add customer.');
      }
    );
  }

  // Update an existing customer
  updateCustomer(id: number, customer: Customer): void {
    const url = `${this.apiUrl}/${id}`;  // Include the customer ID in the URL
    this.http.put(url, customer).subscribe(
      () => {
        alert('Customer updated successfully!');
        this.getCustomers();
        this.customerForm.reset();
      },
      (error) => {
        console.error('Error updating customer:', error);
        alert('Failed to update customer.');
      }
    );
  }


  // Delete a customer
  deleteCustomer(id: number): void {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe(
        () => {
          alert('Customer deleted successfully!');
          this.getCustomers();
        },
        (error) => {
          console.error('Error deleting customer:', error);
          alert('Failed to delete customer.');
        }
      );
    }
  }
}
