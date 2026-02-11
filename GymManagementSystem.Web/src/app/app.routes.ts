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
import { AddClassBooking } from './components/ClassBooking/add-class-booking/add-class-booking';
import { AboutUs } from './components/about-us/about-us';
import { ActivateAccount } from './components/client-zone/activate-account/activate-account';
import { TrainerLogin } from './components/Trainer/trainer-login/trainer-login';
import { TrainerMain } from './components/Trainer/trainer-main/trainer-main';
import { roleGuardGuard } from './guard/role-guard-guard';
import { TrainerTimeOffAdd } from './components/Trainer/PersonalTrainer/trainer-time-off-add/trainer-time-off-add';
import { ResetClientPassword } from './components/client-zone/reset-client-password/reset-client-password';

export const routes: Routes = [

{
  path: '',
  component: MainPage,
},

{
  path: 'about-us',
  component: AboutUs,
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
  path: 'forgot-password',
  component: ForgotPassword
},
{
  path: 'activate-account',
  component: ActivateAccount
},
{ 
  path: 'reset-client-password',
  component: ResetClientPassword,
},
{
  path: 'client-main-page',
  component: ClientMainPage,
  canActivate: [roleGuardGuard],
  data: { roles: ['Client'] }
},
{
  path: 'add-personal-booking',
  component: AddPersonalBooking,
  canActivate: [roleGuardGuard],
  data: { roles: ['Client'] }
},
{
  path: 'add-class-booking',
  component: AddClassBooking,
  canActivate: [roleGuardGuard],
  data: { roles: ['Client'] }
},
{
  path: 'buy-membership/:id',
  component: BuyMembership,
  canActivate: [roleGuardGuard],
  data: { roles: ['Client'] }
},
{
  path: 'change-password',
  component: ChangePassword,
  canActivate: [roleGuardGuard],
  data: { roles: ['Client'] }
},
{
  path: 'memberships',
  component: Membership
},

{
  path: 'trainer/login',
  component: TrainerLogin
},
{
  path: 'trainer',
  component: TrainerMain,
  canActivate: [roleGuardGuard],
  data: { roles: ['Trainer', 'GroupInstructor'] }
},
{ 
  path: 'trainer-time-off',
  component: TrainerTimeOffAdd,
  canActivate: [roleGuardGuard],
  data: { roles: ['Trainer'] }
},


];
