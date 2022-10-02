import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";

import { ItemService } from '../services/item-services';
import { SecurityService } from '../security/security.service';
import { Item } from '../models/item/item';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {
  title = "Lista de articulos";
  cssProgress = "progress";
  displayedColumns: string[] = ['icono', 'internal', 'articulo', 'barcode', 'suggested', 'sugghigh', 'cost', 'price'];
  items: Item[] = [];

  constructor(private itemService: ItemService,
    private securityService: SecurityService, 
    private router: Router,
    private titleService: Title) { 
      this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);
    this.getAll();
  }

  getAll() {
    this.itemService.getAll().subscribe(items => {this.items = items; this.cssProgress = "progress-hidden";});
  }
}
