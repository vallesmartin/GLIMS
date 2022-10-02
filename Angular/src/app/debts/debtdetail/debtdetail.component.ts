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
import { DialogElementsShippingDialog } from 'src/app/shipping/shipping-lines/shipping-lines.component';

@Component({
  selector: 'app-debtdetail',
  templateUrl: './debtdetail.component.html',
  styleUrls: ['./debtdetail.component.css']
})
export class DebtdetailComponent implements OnInit {
  title = "Factura";
  cssProgress = "progress";
  numerator = new FormControl('', [Validators.required]);
  displayedColumns: string[] = ['CodInt', 'Cantidad', 'Articulo'];
  delivery: Document | undefined;
  deliveryNumber: string = "";
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
      this.deliveryNumber = document.DocumentNumber;
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

  openErrorSnackBar() {
    this._snackBar.open('Para finalizar cobranza, debe introducir un importe.', 'Ok', {
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      duration: 3000
    });
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
}
