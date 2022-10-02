import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from "@angular/platform-browser";
import {FormControl, Validators} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';

import { ItemsComponent } from '../items.component';
import { ItemService } from 'src/app/services/item-services';
import { Item } from 'src/app/models/item/item';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  title = "Articulo";
  cssProgress = "progress";
  item: any;

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
      this.item = itemResponse;
      this.title = "Art: "+this.item.ItemDescription;
      this.titleService.setTitle(this.title);
      this.cssProgress = "progress-hidden";
    });
  }
}
