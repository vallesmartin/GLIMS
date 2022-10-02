import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";

import { EntityService } from '../services/entity-service';
import { SecurityService } from '../security/security.service';
import { Entity } from '../models/entity/entity';

@Component({
  selector: 'app-entities',
  templateUrl: './entities.component.html',
  styleUrls: ['./entities.component.css']
})
export class EntitiesComponent implements OnInit {
  title = "Entidades";
  cssProgress = "progress";
  displayedClientsColumns: string[] = ['InternalCode', 'Description', 'Contact', 'Phone'];
  clients: Entity[] = [];
  displayedSuppliersColumns: string[] = ['id', 'LegalName', 'Address'];
  suppliers: Entity[] = [];

  constructor(private enityService: EntityService,
    private securityService: SecurityService, 
    private router: Router,
    private titleService: Title) { 
      this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);
    this.getEntities();
  }
  getEntities() {
    this.enityService.getByType(1).subscribe(entities => this.clients = entities);
    this.enityService.getByType(2).subscribe(entities => {this.suppliers = entities;this.cssProgress = "progress-hidden";});
  }
}
