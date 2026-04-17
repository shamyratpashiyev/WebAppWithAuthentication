import {Component, OnDestroy, OnInit} from '@angular/core';
import {UserService} from '../../services/user-service';
import {UserDto} from '../../models/user-dto';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-users',
  imports: [],
  templateUrl: './users.html',
  styleUrl: './users.scss',
})
export class Users implements OnInit, OnDestroy {
    constructor(private userService: UserService) {}

    userList: UserDto[] = [];
    private subscriptionList: Subscription[] = [];

    ngOnInit(): void {
      this.subscriptionList[0] = this.userService.getAll()
          .subscribe(res => {
            this.userList = res;
          })
    }


    ngOnDestroy(): void {
      for (const sub of this.subscriptionList) {
        sub.unsubscribe();
      }
    }

}
