export interface IContact {
  id: number;
  firstName: string;
  lastName: string;
}

export class Contact implements IContact {
  constructor(public id: number, public firstName: string, public lastName: string) {}
}
