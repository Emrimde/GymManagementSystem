import { Component, OnInit, signal } from '@angular/core';
import { ClientService } from '../../../services-api/client-service';
import { Router, RouterOutlet, RouterLink } from '@angular/router';
import { ClientDetails } from '../../../dto/Client/client-details';
import { SIGNAL } from '@angular/core/primitives/signals';
import { ClientMembership } from './client-membership/client-membership';
import { ClientReservation } from './client-reservation/client-reservation';
import { ClientPersonalTraining } from './client-personal-training/client-personal-training';

@Component({
  selector: 'app-client-main-page',
  imports: [ClientMembership, ClientReservation, ClientPersonalTraining, RouterLink],
  templateUrl: './client-main-page.html',
  styleUrl: './client-main-page.css',
})
export class ClientMainPage implements OnInit {
  activeTab : 'membership' | 'reservations' | 'personalTrainings' =  'membership'
  
  constructor(private clientService: ClientService, private router: Router){}
  clientResponse = signal<ClientDetails | null>(null);

  ngOnInit(): void {
    this.clientService.getClientDetails().subscribe({
      next: (response:any) =>{
        this.clientResponse.set(response)
      },
      error: (err) => {
        console.error(err)
      }
    })
  }



}
