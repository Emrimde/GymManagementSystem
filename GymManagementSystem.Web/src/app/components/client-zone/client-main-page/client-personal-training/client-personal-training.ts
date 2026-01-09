import { Component, OnInit, signal } from '@angular/core';
import { ClientService } from '../../../../services-api/client-service';
import { ClientMembershipInformationResponse } from '../../../../dto/Client/client-membership-information-response';
import { PersonalBookingService } from '../../../../services-api/personal-booking-service';
import { PersonalBookingResponse } from '../../../../dto/PersonalBooking/personal-booking-response';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-client-personal-training',
  imports: [RouterLink],
  templateUrl: './client-personal-training.html',
  styleUrl: './client-personal-training.css',
})
export class ClientPersonalTraining implements OnInit {
  constructor(private clientService: ClientService, private personalBookingService: PersonalBookingService) {}

  clientMembershipInformation!: ClientMembershipInformationResponse;
  hasActiveMembership = signal<boolean>(false);
  personalBookings = signal<PersonalBookingResponse[]>([]);
  
  ngOnInit(): void {
    this.clientService.getClientContext().subscribe({
      next: (response:any) => {
        this.clientMembershipInformation = response;
        this.hasActiveMembership.set(this.clientMembershipInformation.hasActiveMembership);
      },
      error: (error:any) => {
        console.error(error);
      }
    })
    
    this.personalBookingService.getPersonalBookingsForClient().subscribe({
      next: (response:any) => {
        this.personalBookings.set(response);
        console.log(this.personalBookings());
      },
      error: (error:any) => {
        console.error(error);
      }
    })
  }
}
