import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BassGuitarsComponent } from './bass-guitars.component';

describe('BassGuitarsComponent', () => {
  let component: BassGuitarsComponent;
  let fixture: ComponentFixture<BassGuitarsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BassGuitarsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BassGuitarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
