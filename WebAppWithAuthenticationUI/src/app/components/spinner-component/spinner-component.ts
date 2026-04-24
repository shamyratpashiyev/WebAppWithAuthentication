import {ChangeDetectionStrategy, Component} from '@angular/core';
import {SpinnerService} from '../../services/spinner-service';

@Component({
  selector: 'app-spinner-component',
  imports: [],
  templateUrl: './spinner-component.html',
  styleUrl: './spinner-component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SpinnerComponent {
  constructor(public spinnerService: SpinnerService) {
  }
}
