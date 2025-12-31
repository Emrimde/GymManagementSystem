import { Component, OnInit, signal } from '@angular/core';
import { ClientService } from '../../../services-api/client-service';
import { Router } from '@angular/router';
import { ClientDetails } from '../../../dto/Client/client-details';
import { SIGNAL } from '@angular/core/primitives/signals';

@Component({
  selector: 'app-client-main-page',
  imports: [],
  templateUrl: './client-main-page.html',
  styleUrl: './client-main-page.css',
})
export class ClientMainPage implements OnInit {

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
