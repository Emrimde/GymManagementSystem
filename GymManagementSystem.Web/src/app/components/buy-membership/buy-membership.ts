import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from "@angular/forms";
import { ClientDetails } from '../../dto/Client/client-details';
import { ClientService } from '../../services-api/client-service';
import { ClientMembershipService } from '../../services-api/client-membership-service';
import { email } from '@angular/forms/signals';
import { ClientMembershipPreviewResponse } from '../../dto/ClientMembership/client-membership-preview-response';
import { ClientUpdateRequest } from '../../dto/Client/client-update-request';
import { ClientMembershipAddRequest } from '../../dto/ClientMembership/client-membership-add-request';

@Component({
  selector: 'app-buy-membership',
  imports: [ɵInternalFormsSharedModule,ReactiveFormsModule],
  templateUrl: './buy-membership.html',
  styleUrl: './buy-membership.css',
})
export class BuyMembership implements OnInit {
  clientMembershipForm!: FormGroup;
  id!: string | null;
  clientDetails = signal<ClientMembershipPreviewResponse | null>(null)
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')
    this.clientMembershipForm = this.buildClientMembershipForm()
    this.clientMembershipService.getClientMembershipPreview(this.id!).subscribe({
      next: (response:any) =>{
       response.dateOfBirth = response.dateOfBirth?.slice(0, 10) || null;
       console.log(response.dateOfBirth)
       this.clientMembershipForm.patchValue(response)
       
      },
      error: (error:any) =>{
        console.error(error)
      }
    })
    
  }

  async submit() {
    if(this.clientMembershipForm.invalid){
      return
    } 
    const dto: ClientUpdateRequest = {
      city: this.clientMembershipForm.value.city,
      street: this.clientMembershipForm.value.street,
      phoneNumber: this.clientMembershipForm.value.phoneNumber,
      dateOfBirth: this.clientMembershipForm.value.dateOfBirth
    }

    this.clientService.updateClient(dto).subscribe({
      next: () => {
        const clientMembershipAddRequest: ClientMembershipAddRequest ={
          membershipId: this.id!,
          isFromWeb: true
        }
        this.clientMembershipService.createClientMembership(clientMembershipAddRequest).subscribe()
      },
      error: (error:any) => {
        console.error(error)
      }
    })
  }


  buildClientMembershipForm(): FormGroup {
    return this.fb.group({
      email: ['', [Validators.email, Validators.required]],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      street: ['', [Validators.required]],
      city: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      membershipName: ['', [Validators.required]],
    })
  }

  constructor(private route:ActivatedRoute, private clientService:ClientService, private clientMembershipService:ClientMembershipService, private fb: FormBuilder){}
}
