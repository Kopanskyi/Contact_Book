import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ILogInData } from '../../models/login-data';
import { AuthService } from '../../services/auth.service';

@Component({
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit, OnDestroy {
  private validationMessages: { [key: string]: string } = {
    required: 'This field is required',
    email: 'Incorrect email',
  };

  private subscriptions: Subscription[] = [];

  public errorMessages: any = {};
  public logInForm: FormGroup = new FormGroup({});

  public get canLogIn() {
    return this.logInForm.dirty && this.logInForm.valid;
  }

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
    ) {
    this.createForm();
  }

  public ngOnInit(): void {
    this.valuesValidation();
  }

  public ngOnDestroy(): void {
    this.subscriptions.forEach(item => item.unsubscribe());
  }

  private createForm(): void {
    this.logInForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  private valuesValidation(): void {
    let subscription = this.logInForm.valueChanges.subscribe(() =>
      this.setMessage(this.logInForm)
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

  public logIn() {
    const logIn: ILogInData = {...this.logInForm.value};

    if (logIn.email && logIn.password) {
      this.authService.logIn(logIn).subscribe({
        next: () => this.router.navigate(['']),
        error: error => this.errorMessages.logIn = error?.error?.message
      });
    }
  }
}
