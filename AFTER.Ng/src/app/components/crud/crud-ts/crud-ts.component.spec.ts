import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrudTsComponent } from './crud-ts.component';

describe('CrudTsComponent', () => {
  let component: CrudTsComponent;
  let fixture: ComponentFixture<CrudTsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CrudTsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CrudTsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
