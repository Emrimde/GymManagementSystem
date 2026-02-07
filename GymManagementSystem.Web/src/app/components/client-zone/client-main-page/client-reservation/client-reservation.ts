import { Component, OnInit, signal } from '@angular/core';
import { ClientService } from '../../../../services-api/client-service';
import { ClientMembershipInformationResponse } from '../../../../dto/Client/client-membership-information-response';
import { ClassBookingService } from '../../../../services-api/class-booking-service';
import { ClassBookingResponse } from '../../../../dto/ClassBooking/class-booking-response';
import { RouterLink } from '@angular/router';
import { ScheduledClassService } from '../../../../services-api/scheduled-class-service';

@Component({
  selector: 'app-client-reservation',
  imports: [RouterLink],
  templateUrl: './client-reservation.html',
  styleUrl: './client-reservation.css',
})
export class ClientReservation implements OnInit {

  constructor(
    private clientService: ClientService,
    private classBookingService: ClassBookingService,
  ) {}

  clientMembershipInformation!: ClientMembershipInformationResponse;
  scheduledClasses = signal<ClassBookingResponse[]>([]);
  hasActiveMembership = signal<boolean>(false);

  ngOnInit(): void {
    this.clientService.getClientContext().subscribe({
      next: (response: any) => {
        this.clientMembershipInformation = response;
        this.hasActiveMembership.set(
          this.clientMembershipInformation.hasActiveMembership
        );
      }
    });

    this.loadClasses();
  }

  private loadClasses() {
    this.classBookingService.getClientClasses().subscribe({
      next: (response: any) => {
        this.scheduledClasses.set(response);
      }
    });
  }

 cancelBooking(classBookingId: string) {
  const confirmed = confirm(
    'Are you sure you want to cancel your reservation for this class?'
  );

  if (!confirmed) return;

  this.classBookingService
    .deleteClassBooking(classBookingId)
    .subscribe({
      next: () => {
        this.scheduledClasses.update(list =>
          list.filter(item => item.id !== classBookingId)
        );
      },
      error: (err) => {
        console.error(err);
        alert('Failed to cancel reservation');
      }
    });
}
}