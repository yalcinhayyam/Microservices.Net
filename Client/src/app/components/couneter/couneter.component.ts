import { Component } from '@angular/core';
import { Service } from '../../service';

@Component({
  selector: 'app-couneter',
  templateUrl: './couneter.component.html',
  styleUrls: ['./couneter.component.css'],
})
export class CouneterComponent {
  constructor(public Service: Service) {}
}
