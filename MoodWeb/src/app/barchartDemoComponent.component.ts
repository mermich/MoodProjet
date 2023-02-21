import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { MoodentriesChartData } from './mood';


@Component({
  selector: 'mood-root',
  template: `
    <canvas baseChart class="chart"
        [data]="barChartData"
        [options]="barChartOptions"
        [type]="barChartType"
        (chartHover)="chartHovered($event)"
        (chartClick)="chartClicked($event)">
</canvas>`,
  styles: []
})

export class BarchartDemoComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  private httpClient: HttpClient;

  constructor(http: HttpClient) {
    this.httpClient = http;
  }

  public moods : MoodentriesChartData[]| undefined;

  ngOnInit() {
    console.log('ngOnInit');

    // pipe est la methode de manipulation des Obervable.
    this.httpClient.get<MoodentriesChartData[]>('http://localhost:7120/api/GetChartData').subscribe(result => {
      this.moods = result;
      if( this.moods )
      {
        this.barChartData.labels= this.moods.map(e=>e.date);
        this.barChartData.datasets=[
          { data: this.moods.map(e=>e.face1Count), label: 'face 1' },
          { data: this.moods.map(e=>e.face2Count), label: 'face 2' },
          { data: this.moods.map(e=>e.face3Count), label: 'face 3' },
          { data: this.moods.map(e=>e.face4Count), label: 'face 4' }
        ];

        this.chart?.update();
        console.log( this.barChartData.labels);
      }
    }, error =>{ console.error(error)}
    );
  }

  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
  };
  public barChartType: ChartType = 'bar';

  public barChartData: ChartData<'bar'> = {
    datasets: [
      { data: [ 65, 59, 80, 81, 56, 55, 40 ], label: 'Series A' },
      { data: [ 28, 48, 40, 19, 86, 27, 90 ], label: 'Series B' }
    ]
  };

  // events
  public chartClicked({ event, active }: { event?: ChartEvent, active?: {}[] }): void {
    console.log(event, active);
  }

  public chartHovered({ event, active }: { event?: ChartEvent, active?: {}[] }): void {
    //console.log(event, active);
  }
}



export interface MoodentriesChartData {
  face1Count:number;
  face2Count:number;
  face3Count:number;
  face4Count:number;
  date:string;
}
