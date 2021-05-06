import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ComandaListComponent } from './comanda-list.component';

describe('ComandaComponent', () => {
  let component: ComandaListComponent;
  let fixture: ComponentFixture<ComandaListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ComandaListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComandaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
