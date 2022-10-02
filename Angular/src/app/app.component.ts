import { Component, HostBinding, Inject, OnInit } from '@angular/core';
import { OverlayContainer } from '@angular/cdk/overlay';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";
import { ThemeService, AppThemes } from './theme.service';
import { FormControl } from '@angular/forms';
import { DOCUMENT } from '@angular/common';

import { SecurityService } from './security/security.service';
import { AppUserRole } from './security/app-user-role';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Andromeda';
  role: AppUserRole = new AppUserRole();
  isAuth: boolean = false;
  public userName: string | null = "";

  @HostBinding('class') componentCssClass: any;

  constructor(@Inject(DOCUMENT) private document: Document,
    private securityService: SecurityService,
    private router: Router,
    private titleService: Title,
    private overlay: OverlayContainer,
    public overlayContainer: OverlayContainer){

    this.userName = this.securityService.userName;
    this.titleService.setTitle(this.title);
    this.securityService.checkAuth();
  }

  @HostBinding('class') className = '';

  public onSetTheme(e: string) {
    this.overlayContainer.getContainerElement().classList.add(e);
    this.componentCssClass = e;
  }

  ngOnInit(): void {
    this.securityService.validateRole().subscribe( roleResponse => {
      this.role = roleResponse;
      localStorage.setItem('role',JSON.stringify(this.role));
    });
    this.chkTheme();
  }

  ngOnChange(): void {
  }

  isUserValid(): boolean{
    return this.securityService.checkAuth();
  }

  chkTheme() {
    const darkClassName = 'darkMode';
    const lightClassName = 'lightMode';
    var themeClass = localStorage.getItem('theme');
    if (themeClass == lightClassName) {
      this.document.body.style.backgroundColor = "white";
      this.document.body.classList.remove(darkClassName);
      localStorage.setItem('theme', lightClassName);
    } else {
      
      this.document.body.style.backgroundColor = "#000000de";
      this.document.body.classList.add(darkClassName);
      localStorage.setItem('theme', darkClassName);
    }
  }

  toggleThemeMode() {
    const darkClassName = 'darkMode';
    const lightClassName = 'lightMode';
    var themeClass = localStorage.getItem('theme');
    console.log(themeClass);
    if (themeClass != lightClassName) {
      this.document.body.style.backgroundColor = "white";
      this.document.body.classList.remove(darkClassName);
      localStorage.setItem('theme', lightClassName);
    } else {
      
      this.document.body.style.backgroundColor = "#000000de";
      this.document.body.classList.add(darkClassName);
      localStorage.setItem('theme', darkClassName);
    }
  }

  logout() {
    localStorage.removeItem('theme');
    this.userName = "";
    this.securityService.logout();
    this.router.navigate(['/login']);
  }
}
