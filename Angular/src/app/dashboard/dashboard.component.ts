import { Component, OnInit, ViewChild } from '@angular/core';
import { SecurityService } from '../security/security.service';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";

import { Document } from '../models/document/document';
import { DocumentService } from '../services/document.service';
import { AppUserRole } from '../security/app-user-role';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

export interface Tile {
  icon: string;
  cols: number;
  rows: number;
  text: string;
  counter: number;
  link: string;
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  title = "Andromeda";
  cssProgress = "progress";
  role: AppUserRole = new AppUserRole();
  counterSts1: number = 0;
  counterSts2: number = 0;
  counterSts3: number = 0;
  counterSts4: number = 0;
  deliveries: Document[] = [];
  dataSource = new MatTableDataSource<Document[]>();
  activity: Document[] = [];
  hasActivity = false;
  greaterThanZero = false;
  isSmallScreen: boolean = false;
  tiles: Tile[] = [
    {text: 'Preparacion', cols: 1, rows: 1, icon: 'shopping_cart', counter: 0, link: '../deliveries'},
    {text: 'Facturacion', cols: 1, rows: 1, icon: 'receipt', counter: 0, link: '../billing'},
    {text: 'Entregas', cols: 1, rows: 1, icon: 'local_shipping', counter: 0, link: '../shipping'},
    {text: 'Cobranzas', cols: 1, rows: 1, icon: 'drive_file_rename_outline', counter: 0, link: '../debts'}
  ];
  activityColumns: string[] = ['number', 'description','client', 'date', 'status'];

  constructor(private securityService: SecurityService, 
    private documentService: DocumentService, 
    private router: Router,
    private titleService: Title){
      this.titleService.setTitle(this.title);
  }

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  ngAfterViewInit() {
    //this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.securityService.validateRole().subscribe( roleResponse => {
      this.role = roleResponse;
      localStorage.setItem('role',JSON.stringify(this.role));
    });
    console.log(this.role);

    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);
    else{
      this.getByStatus(1);
      this.getByStatus(10);
      this.getByStatus(20);
      this.getByStatus(31);
      this.getActivity();
    }

    this.onResize(undefined);
  }

  onResize(event: any){
    if(event != undefined){
      this.isSmallScreen = (event.target.innerWidth <= 600) ? true : false;
    }else{
      this.isSmallScreen = (window.innerWidth <= 600) ? true : false;
    }
    console.log(window.innerWidth);
    if(window.innerWidth <= 600){
      this.activityColumns = ['number', 'client', 'date', 'status'];
    }else{
      this.activityColumns = ['number', 'description','client', 'date', 'status'];
    }
  }

  ngOnChange() {
  }

  getByStatus(status: number) {
    this.documentService.getByStatus(status).subscribe(document => 
      {
        this.deliveries = document;
        this.deliveries.forEach(d => {
          this.counterSts1 = (d.DocumentStatus == 1) ? this.counterSts1+1: this.counterSts1;
          this.counterSts2 = (d.DocumentStatus == 10) ? this.counterSts2+1: this.counterSts2;
          this.counterSts3 = (d.DocumentStatus == 20) ? this.counterSts3+1: this.counterSts3;
          this.counterSts4 = (d.DocumentStatus == 31) ? this.counterSts4+1: this.counterSts4;
        })
        console.log(this.deliveries);
        this.tiles[0].counter = this.counterSts1;
        this.tiles[1].counter = this.counterSts2;
        this.tiles[2].counter = this.counterSts3;
        this.tiles[3].counter = this.counterSts4;
        this.cssProgress = "progress-hidden";
        this.deliveries
      });
  }
  getActivity() {
    this.documentService.getLastActivity().subscribe(data => {
      this.activity = data;
      this.activity.forEach(d => {
        d._documentStatus_ = this.getStatusForRead(d.DocumentStatus);
      })
      this.hasActivity = (this.activity.length > 0) ? true : false;
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
}
