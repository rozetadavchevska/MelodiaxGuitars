import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedalsComponent } from './pedals.component';

describe('PedalsComponent', () => {
  let component: PedalsComponent;
  let fixture: ComponentFixture<PedalsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PedalsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PedalsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
