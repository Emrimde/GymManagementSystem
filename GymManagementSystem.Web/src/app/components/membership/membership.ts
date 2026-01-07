import { Component, computed, OnInit, signal } from '@angular/core';
import { MembershipService } from '../../services-api/membership-service';
import { MembershipResponse } from '../../dto/Membership/membership-response';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-membership',
  imports: [RouterLink],
  templateUrl: './membership.html',
  styleUrl: './membership.css',
})  
export class Membership implements OnInit {
  memberships = signal<MembershipResponse[]>([])
  monthlyMemberships = computed(() => this.memberships().filter(item => item.isMonthly))
  yearlyMemberships = computed(() => this.memberships().filter(x => !x.isMonthly))
  
  constructor(private membershipService:MembershipService){}
  ngOnInit(): void {
    this.membershipService.getAllMembershipsWithFeatures().subscribe({
      next: (response:any) =>{
        this.memberships.set(response)
      },
      error: (error:any) =>{
        console.error(error)
      }
    })
  }
}
