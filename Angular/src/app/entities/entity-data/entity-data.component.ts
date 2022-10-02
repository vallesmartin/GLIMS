import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from "@angular/platform-browser";
import {FormControl, Validators} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

import { EntityService } from 'src/app/services/entity-service';
import { Entity, EntityClass } from 'src/app/models/entity/entity';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-entity-data',
  templateUrl: './entity-data.component.html',
  styleUrls: ['./entity-data.component.css']
})
export class EntityDataComponent implements OnInit {
  cssProgress = "progress";
  entityData: Entity | undefined;
  entityDataClass : EntityClass = new EntityClass();
  entityId = 0;
  entityDescription = "";
  entityLegalName = "";
  entityAddress = "";
  entityLocation = "";
  entityContact = "";
  entityPhone = "";
  entityMail = "";
  entityInternalCode = "";
  entityType = 0;
  newEntityDescription = new FormControl('');
  newEntityLegalName = new FormControl('');
  newEntityAddress = new FormControl('');
  newEntityLocation = new FormControl('');
  newEntityContact = new FormControl('');
  newEntityPhone = new FormControl('');
  newEntityMail = new FormControl('');
  newEntityInternalCode = new FormControl('');

  constructor(private route: ActivatedRoute, 
    private router: Router, 
    private _snackBar: MatSnackBar,
    private entityService: EntityService, 
    public dialog: MatDialog,
    private titleService: Title) { }

  ngOnInit(): void {
    this.getEntityById();
  }

  getEntityById(){
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.entityService.getById(id).subscribe(ent => {

      this.entityData = ent;
      this.entityId = ent.EntityId;
      this.entityDescription = ent.EntityDescription;
      this.entityLegalName = ent.EntityLegalName;
      this.entityAddress = ent.EntityAddress;
      this.entityLocation = ent.EntityLocation;
      this.entityContact = ent.EntityContact;
      this.entityPhone = ent.EntityPhone;
      this.entityMail = ent.EntityMail;
      this.entityInternalCode = ent.EntityInternalCode;
      this.entityType = ent.EntityType;

      this.cssProgress = "progress-hidden";
    });

  }

  saveChanges(): void{
    this.cssProgress = "progress";

    this.entityDataClass = JSON.parse(JSON.stringify(this.entityData));
    this.entityDataClass.EntityLegalName = this.newEntityLegalName.value;
    this.entityDataClass.EntityContact = this.newEntityContact.value;
    this.entityDataClass.EntityDescription = this.newEntityDescription.value;
    this.entityDataClass.EntityId = Number(this.route.snapshot.paramMap.get('id'));
    this.entityDataClass.EntityInternalCode = this.newEntityInternalCode.value;
    this.entityDataClass.EntityLocation = this.newEntityLocation.value;
    this.entityDataClass.EntityMail = this.newEntityMail.value;
    this.entityDataClass.EntityPhone = this.newEntityPhone.value;
    this.entityDataClass.EntityAddress = this.newEntityAddress.value;

    this.entityService.saveEntity(this.entityDataClass).subscribe(
      e => {
        this.getEntityById();
        this.cssProgress = "progress-hidden";
        }
    );
  }
}
