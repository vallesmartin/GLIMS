import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith} from 'rxjs/operators';
import { of } from 'rxjs';

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
  cssProgress = "progress-hidden";
  displayedClientsColumns: string[] = ['Icono','InternalCode','LegalName', 'Description', 'Contact', 'Phone'];
  clients: Entity[] = [];
  form_entity = new FormControl();
  filterType = new FormControl('');
  filterDesc = new FormControl('');
  typeValue: number = 0;

  comboEntities: Entity[] = [];
  entityOptions: Entity[] = [];
  filteredOptions: Observable<Entity[]> | undefined;
  filteredEntities: Observable<Entity[]> | undefined;
  filteredEntityDescription: string = "";
  isSmallScreen: boolean = false;

  constructor(private enityService: EntityService,
    private securityService: SecurityService, 
    private router: Router,
    private titleService: Title) { 
      this.titleService.setTitle(this.title);
  }

  changeStatus(event: any){
    console.log(event.value);
    this.typeValue = parseInt(event.value);
    this.filterType = event.value;
  }

  ngOnInit(): void {
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);

      this.getEntitiesCombo();

      this.filteredOptions = this.form_entity.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
    
      this.resize();
  }

  resize():void{
    this.isSmallScreen = (window.innerWidth <= 600) ? true : false;
    
    if(this.isSmallScreen){
      this.displayedClientsColumns = ['Icono','InternalCode','LegalName'];
    }else{
      this.displayedClientsColumns = ['Icono','InternalCode','LegalName', 'Description', 'Contact', 'Phone'];
    }
  }

  onResize(event: any){
    this.resize();
  }

  getEntitiesCombo() {
    
    this.comboEntities = [];
    this.cssProgress = "progress";

    this.enityService.getByType(1).subscribe(entities => {
      entities.forEach( e => this.comboEntities.push(e));
    });

    this.enityService.getByType(2).subscribe(entities => {
      entities.forEach( e => this.comboEntities.push(e));
      this.cssProgress = "progress-hidden";
    });

    this.filteredEntities = of(this.comboEntities);
  }

  private _filter(value: string): Entity[] {
    const filterValue = value;
    let description = "";
    this.comboEntities.forEach(entity => {
;     if (entity.EntityId == parseInt(value)){
        description = entity.EntityDescription;
      }
    });

    this.filteredEntityDescription = description;
    return this.comboEntities.filter(ent => ent.EntityLegalName.toLowerCase().includes(filterValue));
  }

  getEntities() {
    this.resize();
    this.cssProgress = "progress";
    this.clients = [];

    console.log(this.filterDesc.value);

    this.enityService.getByType(this.typeValue).subscribe(entities => {
      
      this.clients = entities.filter( e => ( e.EntityId == this.form_entity.value ) || (this.form_entity.value == null || this.form_entity.value == "") &&
                                        ( ((e.EntityDescription > "")? ((e.EntityDescription.toLowerCase().indexOf(this.filterDesc.value.toLowerCase())) < 0 ? false: true) :false) || this.filterDesc.value == null || this.filterDesc.value == "" )       
                                    );
      
      this.cssProgress = "progress-hidden";
    });
  }
}
