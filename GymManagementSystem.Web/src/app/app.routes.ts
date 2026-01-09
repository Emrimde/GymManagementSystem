import { Routes } from '@angular/router';
import { MainPage } from './components/main-page/main-page';
import { ClientLogin } from './components/client-zone/client-login/client-login';
import { ClientRegister } from './components/client-zone/client-register/client-register';
import { ClientMainPage } from './components/client-zone/client-main-page/client-main-page';
import { Membership } from './components/membership/membership';
import { authGuardGuard } from './guard/auth-guard-guard';
import { BuyMembership } from './components/buy-membership/buy-membership';
import { ChangePassword } from './components/client-zone/change-password/change-password';
import { ForgotPassword } from './components/client-zone/forgot-password/forgot-password';
import { AddPersonalBooking } from './components/personal-booking/add-personal-booking/add-personal-booking';

export const routes: Routes = [
{
    path: '',
    component: MainPage, 
    
},
{
    path: 'login-client',
    component: ClientLogin
},
{
    path: 'register-client',
    component: ClientRegister
},
{
    path: 'client-main-page',
    component: ClientMainPage,
    canActivate: [authGuardGuard]
},
{
    path: 'memberships',
    component: Membership
},
{
    path: 'forgot-password',
    component: ForgotPassword
},

{
    path: 'buy-membership/:id',
    component: BuyMembership,
    canActivate: [authGuardGuard]
},
{
    path: 'change-password',
    component: ChangePassword,
    canActivate: [authGuardGuard]
}
,
{
    path: 'add-personal-booking',
    component: AddPersonalBooking,
    canActivate: [authGuardGuard]
}

];
