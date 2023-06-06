import { NgModule } from '@angular/core';
import { ContactListComponent } from './components/contact-list/contact-list.component';
import { ContactDetailComponent } from './components/contact-detail/contact-detail.component';
import { RouterModule } from '@angular/router';
import { ContactRoutingModule } from './contact-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ContactService } from './services/contact.service';

@NgModule({
  declarations: [
    ContactListComponent,
    ContactDetailComponent
  ],
  imports: [
    RouterModule,
    ContactRoutingModule,
    SharedModule
  ],
  providers: [
    ContactService
  ]
})
export class ContactModule { }
