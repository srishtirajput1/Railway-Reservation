import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private baseUrl = 'https://localhost:7277/api/Authentication/login'; // Update with your API URL

  constructor(private http: HttpClient) {}

  // Authenticate customer
  authenticateCustomer(credentials: { email: string; username: string; password: string }): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/login`, credentials);
  }

  // Get customer details by username
  getCustomerByUsername(username: string, headers: HttpHeaders): Observable<Customer> {
    return this.http.get<Customer>(`${this.baseUrl}/user/${username}`, { headers });
  }
}

