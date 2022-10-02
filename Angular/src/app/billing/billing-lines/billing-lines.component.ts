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
  selector: 'app-billing-lines',
  templateUrl: './billing-lines.component.html',
  styleUrls: ['./billing-lines.component.css']
})
export class BillingLinesComponent implements OnInit {
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
    let document: Document | undefined = this.delivery;

    this.documentService.setBilled(document).subscribe(data => {
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
  clipBoard(copiedData: any){
    this.copiedData = "";
    this.delivery?.Detail.forEach( (el) => {
      this.copiedData += el.ItemData.ItemInternalCode + "\t" + el.DocumentLineQty.toString() + "\r";
    });
    console.log("copiado:"+this.copiedData);
    return `${copiedData}`
  }
  openDialog() {
    const dialogRef = this.dialog.open(DialogElementsBillingDialog);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      if (result=="true")
        this.finalize();
    });
  }
}

@Component({
  selector: 'dialog-elements-example-dialog',
  templateUrl: '/dialog-elements-billing-dialog.html',
})
export class DialogElementsBillingDialog {}

