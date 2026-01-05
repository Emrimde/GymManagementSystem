import { Routes } from '@angular/router';
import { MainPage } from './components/main-page/main-page';
import { ClientLogin } from './components/client-zone/client-login/client-login';
import { ClientRegister } from './components/client-zone/client-register/client-register';
import { ClientMainPage } from './components/client-zone/client-main-page/client-main-page';
import { Membership } from './components/membership/membership';

export const routes: Routes = [
{
    path: '',
    component: MainPage
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
    component: ClientMainPage
},
{
    path: 'memberships',
    component: Membership
}

];
