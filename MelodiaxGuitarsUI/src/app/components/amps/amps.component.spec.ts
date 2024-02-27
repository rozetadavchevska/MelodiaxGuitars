import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmpsComponent } from './amps.component';

describe('AmpsComponent', () => {
  let component: AmpsComponent;
  let fixture: ComponentFixture<AmpsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AmpsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AmpsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
