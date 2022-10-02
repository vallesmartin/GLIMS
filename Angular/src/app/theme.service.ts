import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

export enum AppThemes {
	light = 'deeppurple-amber',
  //lightIndigo = 'indigo-pink',
	dark = 'pink-bluegray',
  //darkGreen = 'purple-green',
}

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  currentTheme: AppThemes | undefined;
	currentThemeSubject = new Subject<string>();

  constructor() { }

  toggleTheme(): void {
		if (this.currentTheme === AppThemes.light) {
			this.currentTheme = AppThemes.dark;
			this.currentThemeSubject.next(AppThemes.dark);
		} else {
			this.currentTheme = AppThemes.light;
			this.currentThemeSubject.next(AppThemes.light);
		}
	}

	getTheme(): Observable<string> {
		return this.currentThemeSubject.asObservable();
	}
}
