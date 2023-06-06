import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UrlSegment } from "@angular/router";
import { Observable } from "rxjs";
import { tap} from "rxjs/operators";
import { Url } from "src/app/shared/models/url";
import { Constants } from "../models/constants";
import { ILogInData } from "../models/login-data";
import { IUser } from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public user: IUser | null = null;

  public get isUserLoggedIn() {
    if (this.user) {
      return true;
    }

    return false;
  }

  public get isAdmin() {
    if (this.user && this.user.role === Constants.adminRole) {
      return true;
    }

    return false;
  }

  constructor(
    private http: HttpClient
    ) {}

  public logIn(logInData: ILogInData): Observable<IUser> {

    return this.http.post<IUser>(Url.loginUrl, logInData).pipe(
      tap(user => this.user = user)
    );
  }

  public CanAllowAccess(url: UrlSegment[]) {
    if (!this.user) {
      return false;
    }

    let isUrlForAdminOnly = false;

    url.forEach(segment => {

      isUrlForAdminOnly = Constants.adminsAllowedRoutesOnly.includes(segment.toString());

      if (isUrlForAdminOnly) {
        return;
      }
    });

    if (isUrlForAdminOnly &&
      this.user.role !== Constants.adminRole) {
        return false;
    }

    return true;
  }

}
