import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Data } from './data';

@Component({
  selector: 'child2Way',
  template: `this is the child component bind with a string : <span style="color:fuchsia">{{data?.label}}</span>
  <button (click)="dataChangeCall()">MethodeDepuisChildEvent</button>`
})
export class Child2Way {

  @Input()
  public data: Data | undefined;

  @Output()
  public dataChange = new EventEmitter<Data>();

  dataChangeCall() {
    this.data!.label = "dataChangeCall";
    this.dataChange.emit(this.data);
  }
}