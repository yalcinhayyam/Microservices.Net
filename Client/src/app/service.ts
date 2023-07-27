import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'any',
  useFactory: () => new Service(),
})
export class Service {
  value: BehaviorSubject<number> = new BehaviorSubject(0);

  constructor() {}
  get value$() {
    return this.value;
  }

  setsetValue(value: number) {
    this.value$.next(this.value$.value + value);
  }
}
