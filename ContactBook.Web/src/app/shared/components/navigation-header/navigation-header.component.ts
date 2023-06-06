import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'cb-navigation-header',
  templateUrl: 'navigation-header.component.html',
  styleUrls: ['navigation-header.component.css'],
})
export class NavigationHeaderComponent {
  public get isUserLoggedIn() {
    return this.authService.isUserLoggedIn;
  }

  public get userName() {
    return this.authService.user?.fullName;
  }

  public get userRole() {
    let firstCharacter = this.authService.user?.role[0].toUpperCase();
    let role = this.authService.user?.role;
    return `${firstCharacter}${role?.substring(1)}`;
  }

  constructor (
    private authService: AuthService,
    private router: Router
  ) {}

  public LogIn () {
    if (this.authService.user) {
      this.authService.user = null;
    }

    this.router.navigate(["login"]);
  }
}
