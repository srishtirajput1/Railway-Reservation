import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-search-results',
  standalone: true,
  imports:[FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchResultsComponent {
  cities = ['Pune', 'Nagpur', 'Mumbai', 'Delhi'];
  source = 'Pune';
  destination = 'Nagpur';
  travelDate = '2025-03-22';
  trains = [
    { name: 'NGP HUMSAFAR EX', number: '22141', departureTime: '22:00', departureDate: 'Thu, 30 Nov', duration: '15:05 hrs', arrivalTime: '13:05', arrivalDate: 'Fri, 01 Dec' }
  ];
  
  selectedTrain: any = null;
  newPassenger = { name: '', age: '' };
  passengers: any[] = [];

  searchTrains() {
    // Ideally, fetch from a service
    console.log('Searching for trains from', this.source, 'to', this.destination);
  }

  openBookingModal(train: any) {
    this.selectedTrain = train;
    const modal = document.getElementById('bookingModal');
    if (modal) {
      new bootstrap.Modal(modal).show();
    }
  }

  addPassenger() {
    if (this.newPassenger.name && this.newPassenger.age) {
      this.passengers.push({ ...this.newPassenger });
      this.newPassenger = { name: '', age: '' };
    }
  }

  removePassenger(index: number) {
    this.passengers.splice(index, 1);
  }

  bookTicket() {
    alert(`Ticket booked for ${this.passengers.length} passengers on ${this.selectedTrain.name}`);
  }
}
