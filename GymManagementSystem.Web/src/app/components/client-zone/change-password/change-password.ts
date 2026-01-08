import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services-api/auth-service';
import { ValidationError } from '@angular/forms/signals';

@Component({
  selector: 'app-change-password',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './change-password.html',
  styleUrl: './change-password.css',
})
export class ChangePassword implements OnInit {
  async submit() {
    throw new Error('Method not implemented.');
  }

  constructor(private router: Router, private fb: FormBuilder, private authService: AuthService) { }
  changePasswordForm!: FormGroup
  ngOnInit(): void {
    this.changePasswordForm = this.buildPasswordForm()
  }

  buildPasswordForm() {
    return this.fb.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmPassword: ['', [Validators.required]],
     
    }, { validators: [ this.passwordMatchValidator ] });
  }

  passwordMatchValidator(group: FormGroup): ValidationErrors | null {
    const newPassword = group.get('newPassword')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return newPassword === confirmPassword ? null : { passwordMismatch: true };
  }

}
