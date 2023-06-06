import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInComponent } from './shared/components/log-in/log-in.component';
import { WelcomeComponent } from './shared/components/welcome/welcome.component';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes =
[
  {path: 'welcome', component: WelcomeComponent},
  {
    path: 'contact',
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    loadChildren: () => import('./contacts/contact.module').then(m => m.ContactModule)
  },
  {path: 'login', component: LogInComponent},
  {path: '', redirectTo: 'welcome', pathMatch: 'full'},
  {path: '**', redirectTo: 'welcome', pathMatch: 'full'}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
