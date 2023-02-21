import { Component, Input } from '@angular/core';

@Component({
  selector: 'ouiNon',
  template: `<i *ngIf="val"  class='material-icons'>thumb_up</i><i *ngIf="!val"  class='material-icons'>close</i>`,
  styles: []
})
export class OuiNon  {
  @Input()
  public val:boolean= false;
}