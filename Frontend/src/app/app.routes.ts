import { Routes } from '@angular/router';
import { LandingComponent } from './Components/landing/landing.component';

import { authGuard } from './Services/Auth/auth.guard';
import { LoginComponent } from './Views/Auth/login/login.component';
import { IndiceAdminComponent } from './Views/Admin/indice-admin/indice-admin.component';

export const routes: Routes = [
    {path: '', component:LandingComponent, canActivate: [authGuard]},

    // Auth
    {path: 'Login', component: LoginComponent},

    // Admin
    {path: 'Admin', component: IndiceAdminComponent, canActivate: [authGuard]},
];
