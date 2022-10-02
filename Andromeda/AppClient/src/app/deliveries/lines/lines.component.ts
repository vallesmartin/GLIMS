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
  selector: 'app-lines',
  templateUrl: './lines.component.html',
  styleUrls: ['./lines.component.css']
})
export class LinesComponent implements OnInit {
  title = "Preparacion";
  cssProgress = "progress";
  numerator = new FormControl('', [Validators.required]);
  displayedColumns: string[] = ['Seleccion', 'Cantidad', 'Articulo'];
  delivery: Document | undefined;
  entityName: string = "";
  deliveryLines: DocumentLine[] = [];
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

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
      this.entityName = document.EntityData.EntityLegalName;
      this.title = "Prep: "+this.delivery.EntityData.EntityDescription;
      this.titleService.setTitle(this.title);
      this.cssProgress = "progress-hidden";
    });
  }
  finalize(): void {
    let document: Document | undefined = this.delivery;

    this.documentService.setPrepared(document).subscribe(data => {
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
  openDialog() {
    const dialogRef = this.dialog.open(DialogElementsExampleDialog);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      if (result=="true")
        this.finalize();
    });
  }
}
 
@Component({
  selector: 'dialog-elements-example-dialog',
  templateUrl: '/dialog-elements-example-dialog.html',
})
export class DialogElementsExampleDialog {}