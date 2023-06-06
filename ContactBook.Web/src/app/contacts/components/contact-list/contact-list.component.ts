import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { AuthService } from "src/app/shared/services/auth.service";
import { IContact } from "../../models/contact";
import { ContactService } from "../../services/contact.service";

@Component({
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];

  public contacts: IContact[] = [];
  public errorMessage: string = '';
  public selectedContactId: number | null = null;

  public get isAdmin() {
    return this.authService.isAdmin;
  }

  constructor(
    private contactService: ContactService,
    private authService: AuthService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    let subscription = this.contactService.contactListChangedAction$.subscribe(
      () => this.loadContacts(),
      error => this.errorMessage = error
    );

    this.subscriptions.push(subscription);

    subscription = this.contactService.contactSelectedAction$.subscribe(
      id => this.selectedContactId = id,
      error => this.errorMessage = error
    );

    this.subscriptions.push(subscription);
  }

  public ngOnDestroy(): void {
    this.subscriptions.forEach(element => element.unsubscribe());
  }

  public loadContacts(): void {
    let subscription = this.contactService.getContacts().subscribe(
        contacts => this.contacts = contacts,
        error => this.errorMessage = error
    );

    this.subscriptions.push(subscription);
  }

  public deleteContact(id: number): void {
    debugger;

    let subscription = this.contactService.deleteContact(id).subscribe(
      () => {
        this.loadContacts();

        if (id === this.selectedContactId) {
          this.router.navigate([`/contact`]);
        }
      },
      error => this.errorMessage = error
    );

    this.subscriptions.push(subscription);
  }
}


