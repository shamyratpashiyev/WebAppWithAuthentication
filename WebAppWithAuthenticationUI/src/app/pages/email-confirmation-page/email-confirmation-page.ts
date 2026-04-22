import {Component, OnInit, signal} from '@angular/core';
import {ActivatedRoute, Route, Router, RouterLink} from '@angular/router';
import {AuthService} from '../../services/auth-service';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-email-confirmation-page',
  imports: [
    RouterLink
  ],
  templateUrl: './email-confirmation-page.html',
  styleUrl: './email-confirmation-page.scss',
})
export class EmailConfirmationPage implements OnInit {
  verificationStatus = signal<'loading' | 'success' | 'error'>('loading');

  constructor(private activatedRoute: ActivatedRoute,
              private auth: AuthService) {
  }

  ngOnInit(): void {
    const userId = this.activatedRoute.snapshot.queryParams[environment.emailConfirmation.userIdQueryString];
    const token = this.activatedRoute.snapshot.queryParams[environment.emailConfirmation.tokenQueryString];
    console.log('Token from URL:', token);
  }
}
