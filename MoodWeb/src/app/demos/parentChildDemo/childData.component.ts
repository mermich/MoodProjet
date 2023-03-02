import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Data } from './data';

@Component({
  selector: 'childData',
  template: `Now we have a child component we pass an object: Data<br>
  <span style="color:blue">{{data.id}}-{{data.label}}</span>
  <br>
  This button is on the child and will rise an event on the parent : 
  <button (click)="MethodeDepuisChildEvent('test')">MethodeDepuisChildEvent</button>
  <br>
  This button is on the child and will rise an event on the parent : 
  <button (click)="MethodeDepuisChildEvent2(data)">MethodeDepuisChildEvent</button>`
})
export class ChildData {
  @Input()
  public data: Data = new Data(0, '');

  @Output()
  depuisChildEvent = new EventEmitter<string>();

  @Output()
  depuisChildEventData = new EventEmitter<Data>();

  MethodeDepuisChildEvent(value: string) {
    this.depuisChildEvent.emit(value);
  }

  MethodeDepuisChildEvent2(value: Data) {
    this.depuisChildEventData.emit(value);
  }
}