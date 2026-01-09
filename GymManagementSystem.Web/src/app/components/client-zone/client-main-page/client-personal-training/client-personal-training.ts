import { Component, OnInit, signal } from '@angular/core';
import { ClientService } from '../../../../services-api/client-service';
import { ClientMembershipInformationResponse } from '../../../../dto/Client/client-membership-information-response';

@Component({
  selector: 'app-client-personal-training',
  imports: [],
  templateUrl: './client-personal-training.html',
  styleUrl: './client-personal-training.css',
})
export class ClientPersonalTraining implements OnInit {
  constructor(private clientService: ClientService) {}
  clientMembershipInformation!: ClientMembershipInformationResponse;
  hasActiveMembership = signal<boolean>(false);
  
  ngOnInit(): void {
    this.clientService.getClientContext().subscribe({
      next: (response:any) => {
        this.clientMembershipInformation = response;
        this.hasActiveMembership.set(this.clientMembershipInformation.hasActiveMembership);
        console.log(this.hasActiveMembership);
      },
      error: (error:any) => {
        console.error(error);
      }
    })
    
  }

}
