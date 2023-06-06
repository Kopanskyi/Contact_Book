import { ServerUrl } from './server-url';

export class Url {
  public static readonly contactUrl: string = `${ServerUrl.serverUrl}/api/contact`;
  public static readonly countryUrl: string = `${ServerUrl.serverUrl}/api/country`;
  public static readonly loginUrl: string = `${ServerUrl.serverUrl}/api/account/login`;

}
