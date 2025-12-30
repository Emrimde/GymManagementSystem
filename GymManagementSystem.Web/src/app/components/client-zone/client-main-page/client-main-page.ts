import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../../services-api/client-service';
import { Router } from '@angular/router';
import { ClientDetails } from '../../../dto/Client/client-details';

@Component({
  selector: 'app-client-main-page',
  imports: [],
  templateUrl: './client-main-page.html',
  styleUrl: './client-main-page.css',
})
export class ClientMainPage implements OnInit {

  constructor(private clientService: ClientService, private router: Router){}
  clientResponse!: ClientDetails;

  ngOnInit(): void {
    this.clientService.getClientDetails().subscribe({
      next: (response:any) =>{
        this.clientResponse = response
      },
      error: (err) => {
        console.error(err)
      }
    })
  }



}
