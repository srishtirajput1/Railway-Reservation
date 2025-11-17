import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:7277/api/Authentication/login'; // ✅ Backend API

  constructor(private http: HttpClient) {}

  // login(data: any): Observable<any> {
  //   return this.http.post<any>(this.apiUrl, data);
  // }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token'); // ✅ Check if token exists
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
  }

  login(userData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, userData).pipe(
      tap((response: any) => {
        this.storeToken(response.token);
        this.storeUserRole(response.user.role);
      })
    );
  }
  
}


// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { Observable } from 'rxjs';

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthService {
//   private loginUrl = 'https://localhost:7277/api/Authentication/login';

//   constructor(private http: HttpClient) { }

//   loginUser(user: any):Observable<any>{
//     return this.http.post(`${this.loginUrl}`, user);
//   }
// }


// import { Injectable } from '@angular/core';
// import { Router } from '@angular/router';

// @Injectable({
//   providedIn: 'root',
// })
// export class AuthService {
//   constructor(private router: Router) {}

//   login(customer: any) {
//     localStorage.setItem('authUser', JSON.stringify(customer));
//   }

//   logout() {
//     localStorage.removeItem('authToken');
//     localStorage.removeItem('authUser');
//     localStorage.removeItem('customerEmail');
//     this.router.navigate(['/login']);
//   }

//   isLoggedIn(): boolean {
//     return localStorage.getItem('authToken') !== null;
//   }

//   getUserRole(): string | null {
//     const user = localStorage.getItem('authUser');
//     return user ? JSON.parse(user).role : null;
//   }

//   getCustomerEmail(): string | null {
//     return localStorage.getItem('customerEmail');
//   }
// }
