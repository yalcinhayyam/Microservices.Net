import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CouneterComponent } from './couneter.component';

describe('CouneterComponent', () => {
  let component: CouneterComponent;
  let fixture: ComponentFixture<CouneterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CouneterComponent]
    });
    fixture = TestBed.createComponent(CouneterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
