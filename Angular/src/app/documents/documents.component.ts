import { Component,  OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
import { map, startWith} from 'rxjs/operators';

import { DocumentService } from '../services/document.service';
import { SecurityService } from '../security/security.service';
import { Document } from '../models/document/document';
import { Entity } from '../models/entity/entity';
import { EntityService } from '../services/entity-service';
import { DocumentFilter } from '../models/document/document-filter';


@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.css']
})
export class DocumentsComponent implements OnInit {
  title = "Busqueda de pedidos";
  cssProgress = "progress";
  cssProgressTable = "progress-hidden";
  displayedColumns: string[] = ['icono', 'numero', 'cliente', 'fecha', 'cantidad', 'importe', 'estado'];
  deliveries: Document[] = [];
  counterDetail = 0;
  form_description = new FormControl();
  form_entity = new FormControl();
  entityOptions: Entity[] = [];
  entities: Entity[] = [];

  filters = new DocumentFilter();
  filterDocNum = new FormControl('');
  filterDocSts = new FormControl('');
  filterDocLastDate = new FormControl('');
  filteredOptions: Observable<Entity[]> | undefined;
  filteredEntities: Observable<Entity[]> | undefined;
  filteredEntityDescription: string = "";
  isSmallScreen: boolean = false;

  constructor(private documentService: DocumentService,
    private securityService: SecurityService, 
    private enityService: EntityService,
    private router: Router,
    private titleService: Title) {
      this.titleService.setTitle(this.title);
  }
  
  ngOnInit(): void {
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);

    this.getEntities();
    
    this.filteredOptions = this.form_entity.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
    this.onResize(undefined);
  }

  onResize(event: any){
    this.isSmallScreen = (window.innerWidth <= 600) ? true : false;
    console.log("change:"+this.isSmallScreen);
    if(this.isSmallScreen){
      this.displayedColumns= ['icono', 'numero', 'cliente', 'fecha'];
    }else{
      this.displayedColumns= ['icono', 'numero', 'cliente', 'fecha', 'cantidad', 'importe', 'estado'];
    }
  }

  private _filter(value: string): Entity[] {
    const filterValue = value;

    this.entities.forEach(entity => {
;      if (entity.EntityId == parseInt(value))
        this.filteredEntityDescription = entity.EntityDescription;
    });

    return this.entityOptions.filter(ent => ent.EntityDescription.toLowerCase().includes(filterValue));
  }

  getEntities() {
    this.cssProgress = "progress";
    this.filteredEntities = this.enityService.getByType(1);
    this.enityService.getByType(1).subscribe(ents => {
      
      this.entities = ents;
      this.entities.forEach(entity => this.entityOptions.push(entity));
      this.cssProgress = "progress-hidden";
    });
  }

  changeStatus(data: any) {
    this.filters.DocumentStatus = data.value;
  }

  getByFilter() {
    this.cssProgressTable = "progress";
    // Completa filtro para servicio filtrado
    this.filters.DocumentNumber = this.filterDocNum.value;
    this.filters.EntityId = this.form_entity.value;
    this.filters.DocumentLastUpdateAt = this.filterDocLastDate.value;
    
    // Llamada servicio
    this.documentService.getByFilter(this.filters).subscribe( docs => {
      this.deliveries = docs;
      this.cssProgressTable = "progress-hidden";
    });
  }
}
