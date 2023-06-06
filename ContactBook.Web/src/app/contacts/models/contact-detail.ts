export interface IContactDetail {
  id: number;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
  gender: string;
  countryId: number;
  city: string;
  address: string;
}

export class ContactDetail implements IContactDetail {
  constructor(
    public id: number = 0,
    public firstName: string = '',
    public lastName: string = '',
    public phoneNumber: string = '',
    public email: string = '',
    public gender: string = '',
    public countryId: number = 0,
    public city: string = '',
    public address: string = ''
  ) {}
}


