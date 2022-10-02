import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgxCsvParser, NgxCSVParserError } from 'ngx-csv-parser';
import {FormControl} from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';

import { PriceHistoryLine } from 'src/app/models/item/price';
import { EntityService } from 'src/app/services/entity-service';
import { Entity } from 'src/app/models/entity/entity';
import { Item } from 'src/app/models/item/item';

@Component({
  selector: 'app-prices',
  templateUrl: './prices.component.html',
  styleUrls: ['./prices.component.css']
})
export class PricesComponent implements OnInit {
  title = "Actualizacion de costos/precios";
  cssProgress = "progress-hidden";
  form_description = new FormControl();
  form_entity = new FormControl();
  entityOptions: string[] = [];
  filteredOptions: Observable<string[]> | undefined;
  suppliers: Entity[] = [];
  csvRecords: any[] = [];
  header: boolean = false;
  displayedColumns: string[] = ['internal', 'external', 'item', 'supplier', 'oldcost', 'newcost', 'oldprice', 'newprice'];
  pricesLines: PriceHistoryLine[] = [];

  constructor(private ngxCsvParser: NgxCsvParser,
              private enityService: EntityService ) {}

  @ViewChild('fileImportInput') fileImportInput: any;
  @Input() items: Item[] = [];

  ngOnChange(): void{
    
  }

  ngOnInit(): void {
    
    this.getEntities();
    this.filteredOptions = this.form_entity.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  getEntities() {
    this.cssProgress = "progress";
    this.enityService.getByType(2).subscribe(entities => {
      
      this.suppliers = entities;
      this.suppliers.forEach(entity => this.entityOptions.push(entity.EntityLegalName));
      this.cssProgress = "progress-hidden";
    });
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    console.log(value);
    return this.entityOptions.filter(option => option.toLowerCase().includes(filterValue));
  }

  fileChangeListener($event: any): void {

    
    const files = $event.srcElement.files;
    this.header = (this.header as unknown as string) === 'true' || this.header === true;

    this.ngxCsvParser.parse(files[0], { header: this.header, delimiter: ',' })
      .pipe().subscribe((result: any) => {
        this.pricesLines = [];
        this.csvRecords = result;
        this.csvRecords.forEach( (row: any[] )=> {
          var pricesline = new PriceHistoryLine();
          // price 
          pricesline.PriceHisLItemInternalCode = row[0];
          pricesline.PriceHisLNewCost = row[1];
          pricesline.PriceHisLNewPrice = row[2];
          // item parent
          var isAdd = false;
          for (let element of this.items){
            if(element.ItemInternalCode == pricesline.PriceHisLItemInternalCode){
              isAdd = true;
              pricesline.PriceHisLItemDescription = element.ItemDescription;
              pricesline.PriceHisLItemExternalCode = element.ItemExternalCode;
              pricesline.PriceHisLOldCost = element.ItemCost;
              pricesline.PriceHisLOldPrice = element.ItemCost;
              pricesline.PriceHisLSupplierDescription = element.EntityData.EntityLegalName;
              this.pricesLines.push(pricesline);
              break;
            }
          }
        });
        console.log(JSON.stringify(this.pricesLines));
      }, (error: NgxCSVParserError) => {
        console.log('Error', error);
      });
  }

}
