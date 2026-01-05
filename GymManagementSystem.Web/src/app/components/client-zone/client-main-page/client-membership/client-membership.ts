import { Component, OnInit, signal } from '@angular/core';
import { ClientMembershipService } from '../../../../services-api/client-membership-service';
import { ClientMembershipResponse } from '../../../../dto/ClientMembership/client-membership-response';

@Component({
  selector: 'app-client-membership',
  imports: [],
  templateUrl: './client-membership.html',
  styleUrl: './client-membership.css',
})
export class ClientMembership implements OnInit {
  constructor(private clientMembershipService: ClientMembershipService){}
  clientMembershipResponse = signal<ClientMembershipResponse | null>(null)

  ngOnInit(): void {
    this.clientMembershipService.getClientMembershipInfo().subscribe({
      next: (response:any) =>{
        this.clientMembershipResponse.set(response)
      },
      error: (err:any) => {
        console.error(err)
      }
    })
  }
}
