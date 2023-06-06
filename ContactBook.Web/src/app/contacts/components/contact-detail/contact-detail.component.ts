import { Component, OnDestroy, OnInit } from '@angular/core';
import { IContactDetail } from '../../models/contact-detail';
import { ContactService } from '../../services/contact.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Country } from '../../../shared/models/country';
import { CountryService } from 'src/app/shared/services/country.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.css'],
})
export class ContactDetailComponent implements OnInit, OnDestroy {
  private validationMessages: { [key: string]: string } = {
    required: 'This field is required',
    email: 'Incorrect email'
  };

  private subscriptions: Subscription[] = [];

  public errorMessages: any = {};
  public contactDetailForm: FormGroup = new FormGroup({});
  public contact: IContactDetail | null = null;
  public countries: Country[] = [];
  public errorMessage: string = '';

  public get isAdmin() {
    if (this.authService.isAdmin) {
      return true;
    }

    return false;
  }

  public get canSave() {
    return this.contactDetailForm.dirty && this.contactDetailForm.valid;
  }

  constructor(
    private contactService: ContactService,
    private countryService: CountryService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
    route: ActivatedRoute
  ) {
    this.createForm();

    let subscription = route.params.subscribe((params) => {

      if (params.hasOwnProperty('id')) {
        const id = params['id'];
        this.contactChangeSubscribe(id);
      }
    });

    this.subscriptions.push(subscription);
    this.getStaticData();
  }

  public ngOnInit(): void {
    this.valuesValidation();
  }

  public ngOnDestroy(): void {
    this.subscriptions.forEach((element) => element.unsubscribe());
  }

  private createForm(): void {
    this.contactDetailForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      gender: ['', Validators.required],
      countryId: [null, Validators.required],
      city: ['', Validators.required],
      address: ['', Validators.required],
    });
  }

  private contactChangeSubscribe(id: number): void {
    let subscription = this.contactService.getContact(id).subscribe({
      next: (contact) => {
        this.contact = contact;

        this.contactDetailForm.setValue({
          firstName: this.contact.firstName,
          lastName: this.contact.lastName,
          phoneNumber: this.contact.phoneNumber,
          email: this.contact.email,
          gender: this.contact.gender,
          countryId: this.contact.countryId,
          city: this.contact.city,
          address: this.contact.address,
        });

        if (!this.authService.isAdmin) {
          this.contactDetailForm.disable();
        }

        this.contactService.contactSelected(this.contact.id);
      },
      error: (error) => (this.errorMessage = error),
    });

    this.subscriptions.push(subscription);
    this.contactDetailForm.reset();
  }

  private getStaticData(): void {
    let subscription = this.countryService.getCountries().subscribe({
      next: (countries) => (this.countries = countries),
      error: (error) => (this.errorMessage = error),
    });

    this.subscriptions.push(subscription);
  }

  private valuesValidation(): void {
    let subscription = this.contactDetailForm.valueChanges.subscribe(() =>
      this.setMessage(this.contactDetailForm)
    );

    this.subscriptions.push(subscription);
  }

  public setMessage(group: FormGroup): void {
    for (const controlKey of Object.keys(group.controls)) {
      let control = group.controls[controlKey];
      this.errorMessages[controlKey] = '';

      if (control.dirty && control.errors) {
        this.errorMessages[controlKey] = Object.keys(control.errors)
          .map((key) => this.validationMessages[key])
          .join(' ');
      }
    }
  }

  public saveContact(): void {
    const contact: IContactDetail = { ...this.contact, ...this.contactDetailForm.value };

    if (!contact.id) {
      let subscription = this.contactService.createContact(contact).subscribe({
        next: (id) => this.onSaveComplete(id),
        error: (error) => (this.errorMessage = error),
      });

      this.subscriptions.push(subscription);
      return;
    }

    let subscription = this.contactService.updateContact(contact).subscribe({
      next: () => this.onSaveComplete(contact.id),
      error: (error) => (this.errorMessage = error),
    });

    this.subscriptions.push(subscription);
  }

  public onSaveComplete(id: number = 0): void {
    this.contactDetailForm.markAsPristine();
    this.contactService.contactListChanged();

    if (id > 0) {
      this.router.navigate([`/contact`, id]);
      return;
    }

    this.router.navigate([`/contact`]);
    this.contactService.contactSelected(null);
  }
}
