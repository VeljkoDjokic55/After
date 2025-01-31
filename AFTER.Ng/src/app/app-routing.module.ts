import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './services/guards/auth-guard';


//Components
import { PublicLayoutComponent } from './layout/public-layout/public-layout.component';
import { PrivateLayoutComponent } from './layout/private-layout/private-layout.component';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { CrudComponent } from './components/crud/crud.component';


const routes: Routes = [
    // Public layout
    {
      path: '',
      component: PublicLayoutComponent,
      children: [
        { path: '', component: LoginComponent, pathMatch: 'full' },
        { path: 'forgot-password', component: ForgotPasswordComponent, pathMatch: 'full'},
      ]
    },
  
    // Private layout
    {
      path: '',
      component: PrivateLayoutComponent,
      children: [
        { path: 'dashboard', component: DashboardComponent, pathMatch: 'full', canActivate: [AuthGuard] },
        { path: 'user-management', component: UserManagementComponent, pathMatch: 'full', canActivate: [AuthGuard] },

      ]
    },

    {
      path: '',
      component: PrivateLayoutComponent,
      children: [
        { path: 'crud', component: CrudComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      ]
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
