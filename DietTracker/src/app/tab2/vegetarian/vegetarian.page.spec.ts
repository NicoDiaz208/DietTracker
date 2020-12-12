import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VegetarianPage } from './vegetarian.page';

describe('VegetarianPage', () => {
  let component: VegetarianPage;
  let fixture: ComponentFixture<VegetarianPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VegetarianPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VegetarianPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
