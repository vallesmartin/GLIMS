import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from "@angular/platform-browser";
import { MatTableDataSource } from '@angular/material/table';

import { SecurityService } from '../security.service';
import { UserService } from 'src/app/services/user-service';
import { AppUser, User } from '../app-user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  title = "Usuarios del sistema";
  cssProgress = "progress";
  dataSource = new MatTableDataSource<AppUser>();
  displayedColumns: string[] = ['icono', 'id', 'username', 'seller', 'picker', 'biller', 'deliverer','admin'];
  users: AppUser[] = [];

  constructor(private userService: UserService,
    private securityService: SecurityService, 
    private router: Router,
    private titleService: Title) { 
      this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);

    this.getAll();

    this.dataSource.filterPredicate = (data, filter: string) => {
      return (data.Username.indexOf(filter) !== -1)
     };
  }

  getAll() {
    this.userService.getAll().subscribe(usersResponse => {
      this.users = usersResponse; 
      this.dataSource.data = this.users;
      this.cssProgress = "progress-hidden";
    });
  }

  setupFilter(column: string) {
    this.dataSource.filterPredicate = (d, filter: string) => {
      const textToSearch = d.Username || '';
      return textToSearch.indexOf(filter) !== -1;
    };
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}

export interface UsersTableElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}