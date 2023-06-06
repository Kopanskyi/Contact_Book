import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Url } from "../models/url";
import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private urlsToNotUse = [
    Url.loginUrl
  ]

  constructor(
    private authService: AuthService
    ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (!this.urlsToNotUse.includes(req.url)) {
      const clonedReq = req.clone({
        setHeaders: {
          "user-token": this.authService.user ? this.authService.user.token : ''
        }
      });

      return next.handle(clonedReq);
    }

    return next.handle(req);
  }
}
