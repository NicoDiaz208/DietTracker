import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { AchivmentPage } from './achivment.page';

describe('AchivmentPage', () => {
  let component: AchivmentPage;
  let fixture: ComponentFixture<AchivmentPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AchivmentPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(AchivmentPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
