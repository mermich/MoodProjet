import { Component, OnInit } from '@angular/core';
import { BigData, Data } from './data';

@Component({
  selector: 'parent',
  template: `child component :<br>
  <child [dataLabel]="data.label"></child>
  <br>
  Button on the parent that changes the value, and biding refresh child value :<button (click)="updateChild1()">appendLabel</button>
  <br><br><br><br>
  child data :<childData [data]="bigData.datas[0]" 
    (depuisChildEvent)="eventFromDataChild1($event)">
  </childData>
    <br><br><br><br>
    <child2Way [(data)]="bigData.datas[1]" ></child2Way>
    <br><br><br><br>
    <button (click)="reset2Way()">reset2Way</button>

  `
})
export class Parent implements OnInit {
  public data: Data = new Data(1, 'test');
  public bigData: BigData = new BigData();

  clicks = 0;
  ngOnInit() {

    this.data.label = "coucou";
  }

  updateChild1() {
    this.clicks++;
    this.data.label = "appendLabel" + this.clicks;
  }

  eventFromDataChild1(value: string) {
    this.clicks++;
    this.bigData.datas[0].label = value + this.clicks;
    this.bigData.datas[0].id = this.clicks;
  }

  eventFromDataChild2(value: Data) {
    this.clicks++;
    this.bigData.datas[0].label = value.label + this.clicks;
    this.bigData.datas[0].id = this.clicks;
  }

  reset2Way() {
    this.bigData.datas[1].label = 'old label was ' + this.bigData.datas[1].label;
  }
}