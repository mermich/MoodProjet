import { Component, Input, } from '@angular/core';

@Component({
  selector: 'child',
  template: `this is the child component bind with a string : <span style="color:fuchsia">{{dataLabel}}</span>`
})
export class Child {
  @Input()
  public dataLabel: string = '';
}