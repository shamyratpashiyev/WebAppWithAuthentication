import {Component, OnDestroy, OnInit, signal, Signal} from '@angular/core';
import {UserService} from '../../services/user-service';
import {UserDto} from '../../models/models';
import {switchMap} from 'rxjs';
import {DatePipe} from '@angular/common';
import {TimeagoModule} from 'ngx-timeago';
import {ConfirmationWindow} from '../../components/confirmation-window/confirmation-window';
import {SwalDirective} from '@sweetalert2/ngx-sweetalert2';
import {toObservable, toSignal} from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-users',
  imports: [
    DatePipe,
    TimeagoModule,
    ConfirmationWindow,
    SwalDirective
  ],
  templateUrl: './users.html',
  styleUrl: './users.scss',
})
export class Users implements OnInit {
  filter = signal<string>('');
  userListSignal = toSignal<UserDto[]>(
    toObservable(this.filter)
      .pipe(
        switchMap(filter => this.userService.getFiltered(filter))
      )
  );

  constructor(public userService: UserService) {
  }

  onSearch(e: any): void {
    this.filter.set(e.target.value);
  }

  ngOnInit(): void {
  }

  blockSelectedUsers(){

  }

  unblockSelectedUsers(){

  }

  deleteSelectedUsers(){

  }

  deleteUnverifiedUsers(){

  }
}
