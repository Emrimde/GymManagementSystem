import { Routes } from '@angular/router';
import { MainPage } from './components/main-page/main-page';
import { ClientLogin } from './components/client-zone/client-login/client-login';
import { ClientRegister } from './components/client-zone/client-register/client-register';

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
}

];
