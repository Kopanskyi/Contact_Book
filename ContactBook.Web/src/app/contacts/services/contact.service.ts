import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { IContact }  from '../models/contact';
import { IContactDetail } from '../models/contact-detail';
import { Url } from "../../shared/models/url";

@Injectable()
export class ContactService {
  private contactListChangedSubject = new BehaviorSubject<any>(null);
  private contactSelectedSubject = new Subject<number | null>();

  public contactListChangedAction$ = this.contactListChangedSubject.asObservable();
  public contactSelectedAction$ = this.contactSelectedSubject.asObservable();

  constructor(private http: HttpClient) {}

  public getContacts(): Observable<IContact[]> {
    return this.http.get<IContact[]>(Url.contactUrl);
  }

  public getContact(id: number): Observable<IContactDetail> {
    return this.http.get<IContactDetail>(`${Url.contactUrl}/${id}`)
  }

  public updateContact(contact: IContactDetail): Observable<any> {
    return this.http.put(Url.contactUrl, contact)
  }

  public createContact(contact: IContactDetail): Observable<number> {
    return this.http.post<number>(Url.contactUrl, contact)
  }

  public deleteContact(id: number): Observable<any> {
    return this.http.delete(`${Url.contactUrl}/${id}`)
  }

  public contactListChanged() {
    this.contactListChangedSubject.next(null);
  }

  public contactSelected(id: number | null) {
    this.contactSelectedSubject.next(id);
  }
}
