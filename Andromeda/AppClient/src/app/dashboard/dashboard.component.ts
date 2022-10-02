import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../security/security.service';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";

import { Document } from '../models/document/document';
import { DocumentService } from '../services/document.service';
import { AppUserRole } from '../security/app-user-role';

export interface Tile {
  icon: string;
  cols: number;
  rows: number;
  text: string;
  counter: number;
  link: string;
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  title = "Andromeda";
  cssProgress = "progress";
  role: AppUserRole = new AppUserRole();
  counterSts1: number = 0;
  counterSts2: number = 0;
  counterSts3: number = 0;
  counterSts4: number = 0;
  deliveries: Document[] = [];
  activity: Document[] = [];
  hasActivity = false;
  isSmallScreen: boolean = false;
  tiles: Tile[] = [
    {text: 'A Preparar', cols: 1, rows: 1, icon: 'shopping_cart', counter: 0, link: '../deliveries'},
    {text: 'Preparado', cols: 1, rows: 1, icon: 'receipt', counter: 0, link: '../billing'},
    {text: 'Entrega', cols: 1, rows: 1, icon: 'local_shipping', counter: 0, link: '../billing'},
    {text: 'Firmas', cols: 1, rows: 1, icon: 'border_color', counter: 0, link: '../billing'}
  ];
  activityColumns: string[] = ['number', 'client', 'date', 'status'];

  constructor(private securityService: SecurityService, 
    private documentService: DocumentService, 
    private router: Router,
    private titleService: Title){
      this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
    this.securityService.validateRole().subscribe( roleResponse => {
      this.role = roleResponse;
      localStorage.setItem('role',JSON.stringify(this.role));
    });
    console.log(this.role);
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);
    else{
      this.getByStatus(1);
      this.getByStatus(10);
      this.getByStatus(20);
      this.getActivity();
    }
    this.isSmallScreen = (window.innerWidth <= 400) ? false : true;
  }
  ngOnChange() {
    console.log("change:"+this.role);
  }
  getByStatus(status: number) {
    this.documentService.getByStatus(status).subscribe(document => 
      {
        this.deliveries = document;
        this.deliveries.forEach(d => {
          this.counterSts1 = (d.DocumentStatus == 1) ? this.counterSts1+1: this.counterSts1;
          this.counterSts2 = (d.DocumentStatus == 10) ? this.counterSts2+1: this.counterSts2;
          this.counterSts3 = (d.DocumentStatus == 20) ? this.counterSts3+1: this.counterSts3;
          this.counterSts4 = (d.DocumentStatus == 30) ? this.counterSts4+1: this.counterSts4;
        })
        this.tiles[0].counter = this.counterSts1;
        this.tiles[1].counter = this.counterSts2;
        this.tiles[2].counter = this.counterSts3;
        this.tiles[3].counter = this.counterSts4;
        this.cssProgress = "progress-hidden";
      });
  }
  getActivity() {
    this.documentService.getLastActivity().subscribe(data => {
      this.activity = data;
      this.hasActivity = (this.activity.length > 0) ? true : false;
    });
  }
  onResize(event: any){
    this.isSmallScreen = (event.target.innerWidth <= 400) ? false : true;
  }
}
