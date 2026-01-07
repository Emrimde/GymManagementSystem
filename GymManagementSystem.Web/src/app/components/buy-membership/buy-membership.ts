import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from "@angular/forms";
import { ClientDetails } from '../../dto/Client/client-details';
import { ClientService } from '../../services-api/client-service';
import { ClientMembershipService } from '../../services-api/client-membership-service';
import { email } from '@angular/forms/signals';

@Component({
  selector: 'app-buy-membership',
  imports: [ɵInternalFormsSharedModule,ReactiveFormsModule],
  templateUrl: './buy-membership.html',
  styleUrl: './buy-membership.css',
})
export class BuyMembership implements OnInit {
  clientMembershipForm!: FormGroup;
  clientDetails = signal<ClientDetails | null>(null)
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')
    this.clientMembershipForm = this.buildClientMembershipForm()
    this.clientService.getClientDetails().subscribe({
      next: (response:any) =>{
       this.clientMembershipForm.patchValue(response)
      },
      error: (error:any) =>{
        console.error(error)
      }
    })
    
  }

  submit() {
  throw new Error('Method not implemented.');
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
