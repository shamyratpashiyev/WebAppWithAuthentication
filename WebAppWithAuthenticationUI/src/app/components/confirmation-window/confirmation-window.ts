import {Component, EventEmitter, Input, InputSignal, Output, Signal} from '@angular/core';
import {SwalComponent, SwalPortalComponent} from '@sweetalert2/ngx-sweetalert2';

@Component({
  selector: 'app-confirmation-window',
  imports: [
    SwalComponent
  ],
  templateUrl: './confirmation-window.html',
  styleUrl: './confirmation-window.scss',
})
export class ConfirmationWindow {
  @Input()
  title: string = 'Are you sure?';

  @Input()
  text: string = 'Are you sure you want to do this?';

  @Input()
  showCancelButton: boolean = true;

  @Output()
  confirm: EventEmitter<void> = new EventEmitter();

  constructor() {
    this.confirmClicked = this.confirmClicked.bind(this);
  }

  confirmClicked(){
    this.confirm.emit();
  }
}
