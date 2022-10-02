import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";
import { Observable } from 'rxjs';
import { map, startWith} from 'rxjs/operators';
import { of } from 'rxjs';
import { FormControl } from '@angular/forms';

import { ItemService } from '../services/item-services';
import { SecurityService } from '../security/security.service';
import { Item } from '../models/item/item';
import { Entity } from '../models/entity/entity';
import { EntityService } from '../services/entity-service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {
  title = "Lista de articulos";
  cssProgress = "progress";
  isSmallScreen: boolean = false;
  displayedColumns: string[] = ['icono', 'internal', 'articulo', 'barcode', 'suggested', 'sugghigh', 'cost', 'price'];
  items: Item[] = [];

  internalCode = new FormControl();
  description = new FormControl();
  form_entity = new FormControl();
  category = new FormControl();
  filterType = new FormControl('');
  filterDesc = new FormControl('');
  typeValue: number = 0;
  comboEntities: Entity[] = [];
  entityOptions: Entity[] = [];
  filteredOptions: Observable<Entity[]> | undefined;
  filteredEntities: Observable<Entity[]> | undefined;
  filteredEntityDescription: string = "";
  

  constructor(private itemService: ItemService,
    private entityService: EntityService,
    private securityService: SecurityService, 
    private router: Router,
    private titleService: Title) { 
      this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
    this.cssProgress = "progress";
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);

    this.getEntitiesCombo();

      this.filteredOptions = this.form_entity.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
      this.cssProgress = "progress-hidden";

    this.onResize(undefined);
  }

  private _filter(value: any): any {
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

  getEntitiesCombo() {
    this.comboEntities = [];

    this.entityService.getByType(2).subscribe( entities => {
      entities.forEach( e => this.comboEntities.push(e));
    });

    this.filteredEntities = of(this.comboEntities);
  }

  changeStatus(event: any){
    console.log(event.value);
    this.typeValue = parseInt(event.value);
    this.filterType = event.value;
  }

  getItems(){
    let _internalCode : string = "";
    let _description : string = "";
    this.cssProgress = "progress";
    this.itemService.getAll().subscribe( itemsService => {

      if(this.description.value > ""){
        _description = this.description.value;
      }
      if(this.internalCode.value > ""){
        _internalCode = this.internalCode.value;
      }

      this.items = itemsService.filter( i => (
                                                ( i.ItemInternalCode.toLowerCase().indexOf(_internalCode.toLowerCase()) < 0 ? false :true ) 
                                                || _internalCode == null 
                                                || _internalCode == "" 
                                              ) &&
                                              ( 
                                                ( i.ItemDescription.toLowerCase().indexOf(_description.toLowerCase()) < 0 ? false :true )
                                                || _description == null 
                                                || _description == "" 
                                              ) &&
                                              ( 
                                                i.EntityId == this.form_entity.value
                                                || this.form_entity.value == null
                                                || this.form_entity.value == "" 
                                              )
    )     
      this.cssProgress = "progress-hidden";                               
    });
  }

  onResize(event: any){
    this.isSmallScreen = (window.innerWidth <= 600) ? true : false;
    if(this.isSmallScreen){
      this.displayedColumns= ['icono', 'articulo'];
    }else{
      this.displayedColumns= ['icono', 'internal', 'articulo', 'barcode', 'suggested', 'sugghigh', 'cost', 'price'];
    }
  }
}
