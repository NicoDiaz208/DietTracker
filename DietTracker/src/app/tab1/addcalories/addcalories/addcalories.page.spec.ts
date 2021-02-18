import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { AddcaloriesPage } from './addcalories.page';

describe('AddcaloriesPage', () => {
  let component: AddcaloriesPage;
  let fixture: ComponentFixture<AddcaloriesPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddcaloriesPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(AddcaloriesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
