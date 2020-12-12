import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VeganPage } from './vegan.page';

describe('VeganPage', () => {
  let component: VeganPage;
  let fixture: ComponentFixture<VeganPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VeganPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VeganPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
