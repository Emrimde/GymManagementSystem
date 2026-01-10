import { Component, OnInit, signal } from '@angular/core';
import { ClientService } from '../../../../services-api/client-service';
import { ClientMembershipInformationResponse } from '../../../../dto/Client/client-membership-information-response';
import { ClassBookingService } from '../../../../services-api/class-booking-service';
import { ClassBookingResponse } from '../../../../dto/ClassBooking/class-booking-response';

@Component({
  selector: 'app-client-reservation',
  imports: [],
  templateUrl: './client-reservation.html',
  styleUrl: './client-reservation.css',
})
export class ClientReservation implements OnInit {
  constructor(private clientService: ClientService, private classBookingService: ClassBookingService){}
  clientMembershipInformation!:  ClientMembershipInformationResponse;
  scheduledClasses = signal<ClassBookingResponse[]>([]);
  hasActiveMembership = signal<boolean>(false);

  ngOnInit(): void {
    this.clientService.getClientContext().subscribe({
      next: (response: any) => {
        this.clientMembershipInformation = response;
        this.hasActiveMembership.set(this.clientMembershipInformation.hasActiveMembership);
      },
      error: (error:any) => {
        console.error(error);
      }
    });


    this.classBookingService.getClientClasses().subscribe({
      next: (response: any) => {
        this.scheduledClasses.set(response);
      },
      error: (error:any) => {
        console.error(error);
      }
    })

  }

}
