import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormControl, Validators} from '@angular/forms';
import { Title } from "@angular/platform-browser";
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

import { MatDialog} from '@angular/material/dialog';
import { Document } from 'src/app/models/document/document';
import { DocumentLine } from 'src/app/models/document/doument-line';
import { DocumentService } from 'src/app/services/document.service';

@Component({
  selector: 'app-shipping-lines',
  templateUrl: './shipping-lines.component.html',
  styleUrls: ['./shipping-lines.component.css']
})
export class ShippingLinesComponent implements OnInit {
  title = "Factura";
  cssProgress = "progress";
  numerator = new FormControl('', [Validators.required]);
  displayedColumns: string[] = ['CodInt', 'Cantidad', 'Articulo'];
  delivery: Document | undefined;
  entityName: string = "";
  deliveryLines: DocumentLine[] = [];
  copiedData = "";
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  totalAmount = new FormControl('', [Validators.required]);

  constructor(private route: ActivatedRoute, 
    private router: Router, 
    private _snackBar: MatSnackBar, 
    private documentService: DocumentService, 
    public dialog: MatDialog,
    private titleService: Title){
  }

  ngOnInit(): void {
    this.getDocument();
  }
  getDocument() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.documentService.getById(id).subscribe(document => {
      this.delivery = document;
      this.deliveryLines = this.delivery?.Detail;
      this.entityName = document.EntityData.EntityLegalName;
      this.title = "Fac: "+this.entityName;
      this.titleService.setTitle(this.title);
      this.cssProgress = "progress-hidden";
      this.setDocumentReady();
    });
  }
  setDocumentReady() {
    this.deliveryLines.forEach(line => {
      line.DocumentItemInternalCode = line.ItemData.ItemInternalCode // asigna codigo interno para ordenar
    });
    this.deliveryLines.sort((a, b) => parseFloat(a.DocumentItemInternalCode) - parseFloat(b.DocumentItemInternalCode));
  }
  finalize(): void {
    this.cssProgress = "progress";
    let document: Document | undefined = this.delivery;
    if(document){ // truthy
      document.DocumentTotalAmount = this.totalAmount.value;
    }

    this.documentService.setDelivered(document).subscribe(data => {
      if(data.isOk){
        this.router.navigate(["."]);
      }else{
        this.openSnackBar(data.Message);
      }
      this.cssProgress = "progress-hidden";
    })
  }
  sign(): void {
    let document: Document | undefined = this.delivery;
    if(document){ // truthy
      document.DocumentTotalAmount = this.totalAmount.value;
    }

    this.documentService.setNotIncome(document).subscribe(data => {
      if(data.isOk){
        this.router.navigate(["."]);
      }else{
        this.openSnackBar(data.Message);
      }
    })
  }
  getSelectedClass(row: any): string{
    if (row.checked){
      return "row-checked";
    }else {
      return "row-uncheked";
    }
  }
  openSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      duration: 3000
    });
  }
  openErrorSnackBar() {
    this._snackBar.open('Para Entregar o Firmar debe ingresar importe.', 'Ok', {
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      duration: 3000
    });
  }
  openDialog() {
    if(this.totalAmount.value > 0){
      const dialogRef = this.dialog.open(DialogElementsShippingDialog);
      dialogRef.afterClosed().subscribe(result => {
        if (result=="true")
          this.finalize();
      });
    } else{
      this.openErrorSnackBar();
    }
  }
  openSignDialog() {
    if(this.totalAmount.value > 0){
      const dialogRef = this.dialog.open(DialogElementsShippingDialog);
      dialogRef.afterClosed().subscribe(result => {
        if (result=="true")
          this.sign();
      });
    } else{
      this.openErrorSnackBar();
    }
  }
}

@Component({
  selector: 'dialog-elements-example-dialog',
  templateUrl: '/dialog-elements-shipping-dialog.html',
})
export class DialogElementsShippingDialog {
  public titleDialog:string = "Confirmar"; 

}

