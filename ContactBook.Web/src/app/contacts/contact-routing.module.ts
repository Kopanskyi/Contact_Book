import { NgModule } from '@angular/core';
import { ContactListComponent } from './components/contact-list/contact-list.component';
import { RouterModule, Routes } from '@angular/router';
import { ContactDetailComponent } from './components/contact-detail/contact-detail.component';

const routes: Routes = [
  {
    path: '',
    component: ContactListComponent,
    children: [
      {
        path: 'new',
        component: ContactDetailComponent
      },
      {
        path: ':id',
        component: ContactDetailComponent
      }
    ]
  }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ContactRoutingModule { }
