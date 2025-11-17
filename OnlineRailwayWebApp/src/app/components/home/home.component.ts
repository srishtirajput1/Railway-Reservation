// railway-home.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes} from '@angular/router';
import { Router } from '@angular/router'; 
import { HttpClient, HttpHeaders, provideHttpClient } from '@angular/common/http';

interface Station {
  value: string;
  name: string;
}

@Component({
  selector: 'app-railway-home',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class RailwayHomeComponent implements OnInit {
  searchForm!: FormGroup;
  submitted = false;
  minDate: string;
  errorMessage: string = '';
  
  public getJsonValue: any;
  public postJsonValue: any;
  
  
  stations: Station[] = [
    { value: 'new-delhi', name: 'New Delhi' },
    { value: 'mumbai', name: 'Mumbai Central' },
    { value: 'chennai', name: 'Chennai Central' },
    { value: 'kolkata', name: 'Kolkata Howrah' },
    { value: 'bangalore', name: 'Bangalore City' },
    { value: 'hyderabad', name: 'Hyderabad' },
    { value: 'ahmedabad', name: 'Ahmedabad' },
    { value: 'pune', name: 'Pune' }
  ];
  

  constructor(private formBuilder: FormBuilder,private router: Router, private http: HttpClient) {
    // Set minimum date to today
    const today = new Date();
    this.minDate = today.toISOString().split('T')[0];

    this.searchForm = this.formBuilder.group({
      source: ['', Validators.required],
      destination: ['', Validators.required],
      date: [''] // Date is optional at the beginning
    });
  }

  ngOnInit(): void {
    this.initForm();
    this.getMethod();
  }

  navigateToLogin(): void {
    this.router.navigate(['login']);
  }

  navigateToSearch(): void {
    this.router.navigate(['search']);
  }

  validateAndSearch() {
    const { source, destination, date } = this.searchForm.value;

    if (!source || !destination) {
      this.errorMessage = "Please select both source and destination.";
      return;
    }

    if (!date) {
      this.errorMessage = "Please select a date before searching for trains.";
      return;
    }

    // If everything is valid, clear error message and navigate
    this.errorMessage = '';
    this.router.navigate(['/search'], { queryParams: { source, destination, date } });
  }

  initForm(): void {
    this.searchForm = this.formBuilder.group({
      fromStation: ['', Validators.required],
      toStation: ['', Validators.required],
      travelDate: ['', Validators.required]
    }, {
      validators: this.sameStationValidator
    });
  }

  // Custom validator to check if from and to stations are the same
  sameStationValidator(group: FormGroup) {
    const fromStation = group.get('fromStation')?.value;
    const toStation = group.get('toStation')?.value;
    
    if (fromStation && toStation && fromStation === toStation) {
      return { sameStations: true };
    }
    
    return null;
  }

  // Getter for easy access to form fields
  get f() { 
    return this.searchForm.controls; 
  }

  searchTrains(): void {
    this.submitted = true;

    // Stop here if form is invalid
    if (this.searchForm.invalid) {
      return;
    }

    // Check for same stations error
    if (this.searchForm.hasError('sameStations')) {
      alert('Departure and arrival stations cannot be the same!');
      return;
    }

    // Form is valid, proceed with search
    console.log('Search form submitted:', this.searchForm.value);
    // Here you would typically navigate to results page or make an API call
    alert('Searching for trains... This would redirect to results page in a real application.');
  }

  getMethod(){
    this.http.get('https://localhost:7277/api/Class/get-all').subscribe((data)=>{
      console.log(data);
      this.getJsonValue = data;
    });
  }

  public postMethod(){
    const header = new HttpHeaders({
      contentType: 'application/json'
    })
    let body = {
      title: 'Quick',
      body: 'Code',
      userId: 1,
    };
    this.http.post('https://localhost:7277/api/Class/get-all', body).subscribe((data)=>{
      console.log(data);
      this.postJsonValue = data;
    })
  }
}

