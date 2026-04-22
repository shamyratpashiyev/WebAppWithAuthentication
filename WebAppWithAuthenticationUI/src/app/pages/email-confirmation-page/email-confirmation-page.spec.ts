import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailConfirmationPage } from './email-confirmation-page';

describe('EmailConfirmationPage', () => {
  let component: EmailConfirmationPage;
  let fixture: ComponentFixture<EmailConfirmationPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmailConfirmationPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmailConfirmationPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
