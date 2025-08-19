import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCard } from '@angular/material/card';
import { MatInput } from '@angular/material/input';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatButton } from '@angular/material/button';
import { AccountService } from '../../../core/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule,MatCard,MatFormField,MatLabel,MatInput,MatButton],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
private fb=inject(FormBuilder);
private accountService=inject(AccountService);
private router=inject(Router);
private activeRouter=inject(ActivatedRoute);
  loginForm=this.fb.group({
    email:['',[Validators.required,Validators.email]],
    password:['',[Validators.required,Validators.minLength(8)]],
  });
  returnUrl:string='/shop';
  constructor(){
    const urlParams=this.activeRouter.snapshot.queryParams['returnUrl'];
    if(urlParams){
      this.returnUrl=urlParams;
    }
  }

  onSubmit(){
    if(this.loginForm.valid){
      console.log('Starting login process...');
      this.accountService.login(this.loginForm.value).subscribe({
        next:(response) => {
          console.log('Login response received:', response);
          console.log('Attempting to get user info...');
          this.accountService.getUserInfo().subscribe({
            next: (user) => {
              console.log('User info response:', user);
              if (!user) {
                console.warn('User info is null - possible authentication issue');
              }
              this.router.navigateByUrl(this.returnUrl);
            },
            error: (error) => {
              console.error('Error getting user info:', error);
            }
          });
        },
        error:(error) => {
          console.error('Login failed:', error);
        }
      });
    }
  }
}
