import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CountryService } from './services/country.service';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
  ],
  imports: [
    ReactiveFormsModule
  ],
  providers: [
    CountryService
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
