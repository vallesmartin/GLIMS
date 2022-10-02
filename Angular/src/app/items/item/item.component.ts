import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from "@angular/platform-browser";
import {FormControl, Validators} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';

import { ItemsComponent } from '../items.component';
import { ItemService } from 'src/app/services/item-services';
import { Item, ItemClass } from 'src/app/models/item/item';
import { Observable, of } from 'rxjs';
import { Entity } from 'src/app/models/entity/entity';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  title = "Articulo";
  cssProgress = "progress";
  item: ItemClass = new ItemClass();
  itemData: Item | undefined;
  itemId: number = 0;
  itemDescription: string = "";
  itemExternalCode: string = "";
  itemInternalCode: string = "";
  itemBarcode: string = "";
  itemPrice: number = 0;
  itemCost: number = 0;
  itemSugg: number = 0;
  entityLegalName: string = "";

  newInternalCode = new FormControl('');
  newDescription = new FormControl('');
  newBarcode = new FormControl('');
  newCost = new FormControl('');
  newPrice = new FormControl('');
  newSuggested = new FormControl('');
  newSupplierPack = new FormControl('');


  constructor(private route: ActivatedRoute, 
    private router: Router, 
    private itemsService: ItemService, 
    public dialog: MatDialog,
    private titleService: Title) {

  }

  ngOnInit(): void {
    this.getItem();
  }

  getItem() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.itemsService.getById(id).subscribe( itemResponse => {
      this.itemData = itemResponse;

      this.itemId = itemResponse.ItemId;
      this.itemDescription = itemResponse.ItemDescription;
      this.itemExternalCode = itemResponse.ItemExternalCode;
      this.itemInternalCode = itemResponse.ItemInternalCode;
      this.itemBarcode = itemResponse.ItemBarcode;
      this.itemPrice = itemResponse.ItemPrice;
      this.itemCost = itemResponse.ItemCost;
      this.itemSugg = itemResponse.ItemSugg;
      this.entityLegalName = itemResponse.EntityData.EntityLegalName;


      this.cssProgress = "progress-hidden";
    });
  }

  saveChanges(): void{
    this.cssProgress = "progress";

    this.item.ItemBarcode = this.newBarcode.value;
    this.item.ItemCost = this.newCost.value;
    this.item.ItemDescription = this.newDescription.value;
    this.item.ItemId = Number(this.route.snapshot.paramMap.get('id'));
    this.item.ItemInternalCode = this.newInternalCode.value;
    this.item.ItemPrice = this.newPrice.value;
    this.item.ItemSugg = this.newSuggested.value;

    this.itemsService.save(this.item).subscribe(
      e => {
        this.getItem();
        this.cssProgress = "progress-hidden";
        }
    );
  }
}
