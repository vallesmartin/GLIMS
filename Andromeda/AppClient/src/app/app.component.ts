import { Component, HostBinding, OnInit } from '@angular/core';
import { OverlayContainer } from '@angular/cdk/overlay';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";
import { ThemeService, AppThemes } from './theme.service';

import { SecurityService } from './security/security.service';
import { AppUserRole } from './security/app-user-role';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Andromeda';
  darkThemeEnabled = false;
  role: AppUserRole = new AppUserRole();
  isAuth: boolean = false;
  public userName: string | null = "";

  @HostBinding('class') componentCssClass: any;

  constructor(private securityService: SecurityService,
    private router: Router,
    private titleService: Title,
    private themeService: ThemeService,
    public overlayContainer: OverlayContainer){

    this.userName = this.securityService.userName;
    this.titleService.setTitle(this.title);
    this.securityService.checkAuth();
  }

  public onSetTheme(e: string) {
    this.overlayContainer.getContainerElement().classList.add(e);
    this.componentCssClass = e;
  }

  ngOnInit(): void {
    this.securityService.validateRole().subscribe( roleResponse => {
      this.role = roleResponse;
      localStorage.setItem('role',JSON.stringify(this.role));
    });
  }

  ngOnChange(): void {
  }

  isUserValid(): boolean{
    return this.securityService.checkAuth();
  }

  toggleTheme() {
    this.themeService.toggleTheme();
  }

  logout() {
    this.userName = "";
    this.securityService.logout();
    this.router.navigate(['/login']);
  }
}
