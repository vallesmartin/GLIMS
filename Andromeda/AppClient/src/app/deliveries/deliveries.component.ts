import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";

import { DocumentService } from '../services/document.service';
import { SecurityService } from '../security/security.service';
import { Document } from '../models/document/document';

@Component({
  selector: 'app-deliveries',
  templateUrl: './deliveries.component.html',
  styleUrls: ['./deliveries.component.css']
})
export class DeliveriesComponent implements OnInit {
  title = "Pedidos para preparar";
  cssProgress = "progress";
  displayedColumns: string[] = ['icono', 'numero', 'cliente', 'fecha'];
  deliveries: Document[] = [];

  constructor(private documentService: DocumentService,
    private securityService: SecurityService, 
    private router: Router,
    private titleService: Title) {
      this.titleService.setTitle(this.title);
  }
  
  ngOnInit(): void {
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);
    this.getByStatus(1);
  }

  getByStatus(status: number) {
    this.documentService.getByStatus(status).subscribe(document => {this.deliveries = document;this.cssProgress = "progress-hidden";});
  }
}
