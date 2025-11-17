import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RailwayHomeComponent } from './home.component';

describe('HomeComponent', () => {
  let component: RailwayHomeComponent;
  let fixture: ComponentFixture<RailwayHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RailwayHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RailwayHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
