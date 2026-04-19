import {Component, computed, signal} from '@angular/core';
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
export class Users {
  filter = signal<string>('');
  refreshCount = signal<number>(0);
  allUserSelected = signal<boolean>(false);

  userListSignal = toSignal<UserDto[]>(
    toObservable(computed(() => ({ filter: this.filter(), refresh: this.refreshCount() })
    )).pipe(
        switchMap(({ filter }) => this.userService.getList(filter))
      )
  );
  selectedUserIdList = signal<Set<number>>(new Set());

  constructor(private userService: UserService) {
  }

  onSearch(e: any): void {
    this.filter.set(e.target.value);
  }

  onUserSelect(e: any): void {
    const id = Number(e.target.id);
    this.selectedUserIdList.update(list => {
      if (e.target.checked) {
        list.add(id);
      } else {
        list.delete(id);
      }
      return list;
    });
  }

  onAllUserSelect(e: any): void {
    this.allUserSelected.set(true);
    this.selectedUserIdList.update(list => {
      if (e.target.checked) {
        list = new Set<number>(this.userListSignal()?.map(x => Number(x.id)));
      } else {
        list.clear();
      }
      return list;
    });
  }

  blockSelectedUsers(){
    this.userService.blockSelected([...this.selectedUserIdList()]).subscribe({
      complete: () => {
        this.reloadUsers();
      }
    });
  }

  unblockSelectedUsers(){
    this.userService.unblockSelected([...this.selectedUserIdList()]).subscribe({
      complete: () => {
        this.reloadUsers();
      }
    });
  }

  deleteSelectedUsers(){
    this.userService.deleteSelected([...this.selectedUserIdList()]).subscribe({
      complete: () => {
        this.reloadUsers();
      }
    });
  }

  deleteUnverifiedUsers(){
    this.userService.deleteUnverified().subscribe({
      complete: () => {
        this.reloadUsers();
      }
    });
  }

  private reloadUsers(): void {
    this.selectedUserIdList.set(new Set<number>());
    this.allUserSelected.set(false);
    this.refreshCount.update(x => x + 1);
  }
}
