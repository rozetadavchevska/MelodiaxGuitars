import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ElectricGuitarsComponent } from './electric-guitars.component';

describe('ElectricGuitarsComponent', () => {
  let component: ElectricGuitarsComponent;
  let fixture: ComponentFixture<ElectricGuitarsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ElectricGuitarsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ElectricGuitarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
