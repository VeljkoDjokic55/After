import { Component, OnInit, ViewChild } from '@angular/core';
import { CrudMenuItem } from 'src/app/models/crud.menu.item.model';
import { CrudTsComponent } from './crud-ts/crud-ts.component';

@Component({
  selector: 'app-crud',
  templateUrl: './crud.component.html',
  styleUrls: ['./crud.component.scss']
})
export class CrudComponent implements OnInit {

  public CrudMenuItem = CrudMenuItem;
  crudSelected: CrudMenuItem = CrudMenuItem.TS;

  public searchText: string = '';

  @ViewChild(CrudTsComponent)
  crudTsComponent!: CrudTsComponent;

  constructor() { }

  ngOnInit(): void {
  }

  switchMenuItem(item: CrudMenuItem): void {
    this.crudSelected = item;
  }

  search() : void {
    this.crudTsComponent.filter();
  }

}
