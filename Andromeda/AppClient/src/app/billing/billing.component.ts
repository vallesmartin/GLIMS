import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";

import { DocumentService } from '../services/document.service';
import { SecurityService } from '../security/security.service';
import { Document } from '../models/document/document';

@Component({
  selector: 'app-billing',
  templateUrl: './billing.component.html',
  styleUrls: ['./billing.component.css']
})
export class BillingComponent implements OnInit {
  title = "Preparados - Facturas";
  cssProgress = "progress";
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
    this.getByStatus(10);
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
