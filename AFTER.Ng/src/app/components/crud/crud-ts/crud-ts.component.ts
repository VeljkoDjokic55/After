import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TransmissionStation } from 'src/app/models/transmission.station.model';
import { TsService } from 'src/app/services/ts.service';

@Component({
  selector: 'app-crud-ts',
  templateUrl: './crud-ts.component.html',
  styleUrls: ['./crud-ts.component.scss']
})
export class CrudTsComponent implements OnInit {

  transmissionStations: TransmissionStation[] = [];
  public currentPage: number = 1;
  public pageSize: number = 5;
  public count: number = 0;

  selectedTs: TransmissionStation = new TransmissionStation();
  savePressed: boolean = false;

  @Input() searchText!: string;
  constructor(private tsService: TsService, private toastr: ToastrService) {
    
  }

  ngOnInit(): void {
    this.reloadTable();
  }

  filter() {
    this.currentPage = 1;

    let dataIn = {
      pageInfo:{
        page: this.currentPage,
        pageSize: this.pageSize
      },
      filterParams: {
        SearchValue: this.searchText
      }
    };

    this.tsService.getAll(dataIn).subscribe(
      (response: any) => {
        this.transmissionStations = response.data.data;
        this.count = response.data.count;
        if(this.count == 0) {
          this.toastr.info('No data for given input.');
        }
      },
      error => {
        this.toastr.error('An error ocurred.');
      }
    );
  }

  reloadTable(searchText?: string, page: any = null) {
    if(page) {
      this.currentPage = page;
    }

    let dataIn = {
      pageInfo:{
        page: this.currentPage,
        pageSize: this.pageSize
      },
      filterParams: {
        SearchValue: searchText
      }
    };

    this.tsService.getAll(dataIn).subscribe(
      response => {
        this.transmissionStations = response.data.data;
        this.count = response.data.count;
        if(this.count == 0) {
          this.toastr.info('No data for given input.');
        }
      },
      error => {
        this.toastr.error('An error ocurred.');
      }
    );
  }

  pageChange(value: any) {
    this.currentPage = value;
    this.reloadTable(this.searchText);
  }

  save() {
    this.savePressed = true;
    if(!this.selectedTs.code || !this.selectedTs.name
      || this.selectedTs.code == "" || this.selectedTs.name == "") {
      return;
    }

    this.tsService.save(this.selectedTs).subscribe(
      response => {
        if(response.data == '1') {
          this.toastr.success(response.message);
          this.selectedTs.code = "";
          this.selectedTs.name = "";
          this.reloadTable(this.searchText);
        }
        else {
          this.toastr.error(response.message);
        }
        this.savePressed = false;
      },
      error => {
        this.toastr.error(error);
      }
    );
  }

  selectTs(ts: TransmissionStation) {
    this.selectedTs.name = ts.name;
    this.selectedTs.code = ts.code;
    this.selectedTs.id = ts.id;
  }

  addNew() {
    this.savePressed = false;
    this.selectedTs = new TransmissionStation();
  }

  deleteTs() {
    this.tsService.delete(this.selectedTs).subscribe(
      response => {
        if(response.data == '1') {
          this.toastr.success(response.message);
          this.reloadTable(this.searchText);
        }
        else {
          this.toastr.error(response.message);
        }
      },
      error => {
        this.toastr.error(error);
      }
    );
  }

}
