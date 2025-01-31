import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  assetsList : any[];
  regionsList : any[];
  constructor() {
    this.assetsList = [
      {name :'Select All assets',checked : true},
      {name :'Customers',checked : false},
      {name :'Buildings',checked : false},
      {name :'Transformers',checked : false},
      {name :'DT Poles',checked : false}
    ];
    this.regionsList = [
      {name :'Select all segments',checked : true},
      {name :'Accra East',checked : false},
      {name :'Kumasi',checked : false},
      {name :'Madina',checked : false},
      {name :'Tema',checked : false}
    ];
  }

  shareCheckedList(item:any[]){
    console.log(item);
  }
  shareIndividualCheckedList(item:{}){
    console.log(item);
  }

  ngOnInit(): void {
  }

}
