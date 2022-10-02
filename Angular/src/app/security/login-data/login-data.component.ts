import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";
import {FormControl, Validators} from '@angular/forms';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';

import { AppUser } from '../app-user';
import { AppUserAuth } from '../app-user-auth';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-login-data',
  templateUrl: './login-data.component.html',
  styleUrls: ['./login-data.component.css']
})
export class LoginDataComponent implements OnInit {
  title = "Cambiar contraseña";
  cssProgress = "progress-hidden";
  password = new FormControl('', [Validators.required]);
  passwordConfirmation = new FormControl('', [Validators.required]);
  user: AppUser = new AppUser();
  hide = true;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(private securityService: SecurityService, 
    private router: Router, 
    private _snackBar: MatSnackBar,
    private titleService: Title) {
      this.titleService.setTitle(this.title) 
  }

  ngOnInit(): void {
    
  }

  confirm(){
    let Ok: boolean = true;

    if(this.password.value != this.passwordConfirmation.value){
      this.openSnackBar("Las contraseñas deben coincidir");
      Ok = false;
    }
    if(this.passwordConfirmation.value.length <= 5){
      this.openSnackBar("Confirmacion de contraseña debe tener al menos 5 caracteres");
      Ok = false;
    }
    if(this.password.value.length <= 5){
      this.openSnackBar("Contraseña debe tener al menos 5 caracteres");
      Ok = false;
    }
    if(Ok){
      this.cssProgress = "progress";
      this.securityService.changePass(this.password.value).subscribe(response => {
        this.cssProgress = "progress-hidden";
        this.router.navigate(['/dashboard']);
      },
      (err) => {this.openSnackBar(err);this.cssProgress = "progress-hidden";});
    }
  }

  openSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      duration: 3000
    });
  }

  getErrorMessage() {
    if (this.password.hasError('required')) {
      return 'Debe ingresar una contraseña';
    }
    return this.password.hasError('password') ? 'No es una contraseña valida' : '';
  }

  getErrorPassMessage() {
    if (this.passwordConfirmation.hasError('required')) {
      return 'Debe ingresar una contraseña';
    }
    return this.passwordConfirmation.hasError('passwordConfirmation') ? 'No es una contraseña valida' : '';
  }
}
