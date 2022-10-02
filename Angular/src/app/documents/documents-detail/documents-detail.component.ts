import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from "@angular/platform-browser";
import {FormControl, Validators} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

import { Document } from 'src/app/models/document/document';
import { DocumentLine } from 'src/app/models/document/doument-line';
import { DocumentService } from 'src/app/services/document.service';
import { ApiResponse } from 'src/app/models/api-response';

@Component({
  selector: 'app-documents-detail',
  templateUrl: './documents-detail.component.html',
  styleUrls: ['./documents-detail.component.css']
})
export class DocumentsDetailComponent implements OnInit {
  title = "Preparacion";
  cssProgress = "progress";
  numerator = new FormControl('', [Validators.required]);
  displayedColumns: string[] = ['Cantidad', 'Articulo'];
  delivery: Document | undefined;
  documentDate: Date | undefined;
  documentId: number | undefined;
  documentStatusId: number | undefined;
  documentStatus: string = "";
  documentNumber: string = "";
  counterDetail = 0;
  entityName: string = "";
  deliveryLines: DocumentLine[] = [];
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  panelOpenState = false;

  constructor(private route: ActivatedRoute, 
    private router: Router, 
    private _snackBar: MatSnackBar,
    private documentService: DocumentService, 
    public dialog: MatDialog,
    private titleService: Title) {
      
  }

  ngOnInit(): void {
    this.getDocument();
  }
  getDocument() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.documentService.getById(id).subscribe(document => {
      this.delivery = document;
      this.deliveryLines = this.delivery?.Detail;
      this.documentId = document.DocumentId;
      this.documentStatusId = document.DocumentStatus;
      this.documentStatus = this.getStatusForRead(document.DocumentStatus);
      this.documentNumber = document.DocumentNumber;
      this.documentDate = document.DocumentDate;
      this.entityName = document.EntityData.EntityLegalName;
      this.title = "Prep: "+this.delivery.EntityData.EntityDescription;
      this.titleService.setTitle(this.title);
      this.cssProgress = "progress-hidden";
      this.setDocumentReady();
    });
  }
  getStatusForRead(enumStatus: any): string{
    switch(enumStatus){
      case 1:
        return "Nuevo pedido";
      case 10:
        return "Preparado";
      case 20:
        return "Facturado";
      case 30:
        return "Entrega en cliente";
      case 31:
        return "Firma cliente";
    }
    return "";
  }
  setDocumentReady() {
    this.counterDetail = this.deliveryLines.length;
    this.deliveryLines.forEach(line => {
      line.DocumentItemInternalCode = line.ItemData.ItemInternalCode // asigna codigo interno para ordenar
    });
    this.deliveryLines.sort((a, b) => parseFloat(a.DocumentItemInternalCode) - parseFloat(b.DocumentItemInternalCode));
  }
  goToContext(): void {
    switch (this.documentStatusId) {
      case 1:
        this.router.navigate(['./deliveries/lines', this.documentId]);
        break;
      case 10:
        this.router.navigate(['./billing/billing-lines', this.documentId]);
        break;
      case 20:
        this.router.navigate(['./shipping/shipping-lines', this.documentId]);
        break;
      case 30:
        this.openSnackBar("El pedido se encuentra entregado.");
        break;
      case 31:
        this.router.navigate(['./debts']);
        break;
    
      default:
        this.openSnackBar("El pedido se encuentra en un Estado invalido");
        break;
    }
  }
  getSelectedClass(row: any): string{
    const darkClassName = 'darkMode';
    if (row.checked){
      if (localStorage.getItem('theme') == darkClassName){
        return "row-checked-dark";
      }else{
        return "row-checked";
      }
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
}