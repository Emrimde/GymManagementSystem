import { Component, OnInit } from '@angular/core';
import { GeneralGymDetailService } from '../../services-api/general-gym-detail-service';
import { Router, RouterLink } from '@angular/router';
import { AboutUsResponse } from '../../dto/GeneralGymDetail/about-us-response';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-about-us',
  imports: [AsyncPipe,RouterLink],
  templateUrl: './about-us.html',
  styleUrl: './about-us.css',
})
export class AboutUs implements OnInit {
  constructor(private generalGymService: GeneralGymDetailService, private router: Router){}
  aboutUsContent$!: Observable<AboutUsResponse>;
  ngOnInit(): void {
    this.aboutUsContent$ = this.generalGymService.getAboutUs();
  }
}
  