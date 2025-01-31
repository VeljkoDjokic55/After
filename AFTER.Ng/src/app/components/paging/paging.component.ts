import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.scss']
})
export class PagingComponent implements OnInit {

  @Input() currentPage: number = 1;
  @Input() pageSize: number = 0;
  @Input() count: number = 0;
  public visiblePages: any;
  @Output() pageChange = new EventEmitter();
  
  public isLastPage : boolean = false;
  
  constructor() { }

  ngOnInit(): void {
    this.getVisiblePages();
  }


  getVisiblePages() {
    let lastPageNum = Math.ceil(this.count / this.pageSize);
    this.visiblePages = [];

    if (this.currentPage == 1) {
      this.visiblePages.push(1);
      if (lastPageNum > 1)
        this.visiblePages.push(2);
      if (lastPageNum > 2)
        this.visiblePages.push(3);
      if (lastPageNum > 3)
        this.visiblePages.push(4);
      if (lastPageNum > 4)
        this.visiblePages.push(5);
    }
    else if(this.currentPage == 2) {
      this.visiblePages.push(this.currentPage - 1);
      this.visiblePages.push(this.currentPage);
      console.log(lastPageNum);
      if(lastPageNum >= this.currentPage + 1) {
        this.visiblePages.push(this.currentPage + 1);
      }
      if(lastPageNum >= this.currentPage + 2) {
        this.visiblePages.push(this.currentPage + 2);
      }
      if(lastPageNum >= this.currentPage + 3) {
        this.visiblePages.push(this.currentPage + 3);
      }
    }
    else if(this.currentPage == lastPageNum) {
      if((lastPageNum - 4) > 0) {
        this.visiblePages.push(lastPageNum - 4);
      }
      if((lastPageNum - 3) > 0) {
        this.visiblePages.push(lastPageNum - 3);
      }
      if((lastPageNum - 2) > 0) {
        this.visiblePages.push(lastPageNum - 2);
      }
      if((lastPageNum - 1) > 0) {
        this.visiblePages.push(lastPageNum - 1);
      }
      this.visiblePages.push(this.currentPage);
    }
    else {
      this.visiblePages.push(this.currentPage - 2);
      this.visiblePages.push(this.currentPage - 1);
      this.visiblePages.push(this.currentPage);
      if(lastPageNum >= this.currentPage + 1) {
        this.visiblePages.push(this.currentPage + 1);
      } else {
        this.visiblePages.unshift(this.currentPage - 3);
      }
      if(lastPageNum >= this.currentPage + 1) {
        if(lastPageNum >= this.currentPage + 2) {
          this.visiblePages.push(this.currentPage + 2);
        }
        else {
          this.visiblePages.unshift(this.currentPage - 3);
        }
      } else {
        this.visiblePages.unshift(this.currentPage - 4);
      }
    }

    if(lastPageNum <= this.currentPage){
      this.isLastPage = true;
    }else{
      this.isLastPage = false;
    } 

    return this.visiblePages;
  }

  changePage(pagenum: number) {
    let lastPageNum = Math.ceil(this.count / this.pageSize);
    if (lastPageNum === 0)
      lastPageNum = 1;
    if (pagenum == -1)
      pagenum = lastPageNum;
    if (pagenum == 0)
      pagenum = 1;
    if (pagenum > lastPageNum)
      pagenum = lastPageNum;
    this.currentPage = pagenum;
    this.getVisiblePages();
    this.pageChange.emit(this.currentPage);
  }

}
