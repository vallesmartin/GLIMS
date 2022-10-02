import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user-service';
import { AppUser } from '../../app-user';
import { SecurityService } from '../../security.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  title: string = "Usuario";
  cssProgress = "progress";
  id: number = 0;
  mode : string = "";  
  user: AppUser = new AppUser();

  constructor(private route: ActivatedRoute, 
    private router: Router, 
    private userService: UserService, 
    private securityService: SecurityService,
    public dialog: MatDialog,
    private titleService: Title) {
      this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
    if(!this.securityService.checkAuth())
      this.router.navigate(['/login']);

    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.mode = String(this.route.snapshot.paramMap.get('mode'));
    if (this.mode == "NEW" && this.id == 0){
      this.titleService.setTitle("Usuario: "+this.user.Username);
      this.cssProgress = "progress-hidden";
    }else{
      this.userService.getById(this.id).subscribe(data => {
        this.user = data;
        this.titleService.setTitle("Usuario: "+this.user.Username);
        this.cssProgress = "progress-hidden";
      })
    }
    
  }

}
