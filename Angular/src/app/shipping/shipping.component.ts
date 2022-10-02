import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";

import { DocumentService } from '../services/document.service';
import { SecurityService } from '../security/security.service';
import { Document } from '../models/document/document';

@Component({
  selector: 'app-shipping',
  templateUrl: './shipping.component.html',
  styleUrls: ['./shipping.component.css']
})
export class ShippingComponent implements OnInit {
  title = "Entrega de pedidos";
  cssProgress = "progress";
  isSmallScreen = false;
  displayedColumns: string[] = ['icono', 'numero', 'cliente', 'fecha'];
  deliveries: Document[] = [];
  range = new FormGroup({
    start: new FormControl(),
    end: new FormControl()
  });
  constructor(private documentService: DocumentService, 
    private securityService: SecurityService, 
    private router: Router,
    private titleService: Title){
      this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);
    this.getByStatus(20);
    this.onResize(undefined);
  }

  onResize(event: any){
    this.isSmallScreen = window.innerWidth <= 600 ? true : false;

    if(this.isSmallScreen){
      this.displayedColumns = ['icono', 'numero', 'cliente'];
    }else{
      this.displayedColumns = ['icono', 'numero', 'cliente', 'fecha'];
    }
  }
  getByStatus(status: number) {
    this.documentService.getByStatus(status).subscribe(document => {this.deliveries = document;this.cssProgress = "progress-hidden";});
  }
  applyFilters(){
    const startDate = this.range.get("start")?.value;
    const endDate = this.range.get("start")?.value;
    this.deliveries.filter(d => 
        d.DocumentDate >= startDate &&
        d.DocumentDate <= endDate );
  }
}
  