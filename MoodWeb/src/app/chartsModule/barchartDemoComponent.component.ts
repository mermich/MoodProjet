import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { MoodentriesChartData } from './mood';

@Component({
  selector: 'barchart',
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
    this.httpClient.get<MoodentriesChartData[]>('http://localhost:7120/api/Charts-GetChartData').subscribe(result => {
      this.moods = result;
      if( this.moods )
      {
        this.barChartData.labels= this.moods.map(e=>e.date);
        this.barChartData.datasets=[
          { data: this.moods.map(e=>e.face4Count), label: 'Triste', borderColor: '#FF7F27', backgroundColor: '#FFAF57', fill:true},
          { data: this.moods.map(e=>e.face3Count), label: 'Neutre', borderColor: '#EFE4B0', backgroundColor: '#FFF4E0', fill:true},
          { data: this.moods.map(e=>e.face2Count), label: 'Content', borderColor: '#B5E61D', backgroundColor: '#E5F63D', fill:true},
          { data: this.moods.map(e=>e.face1Count), label: 'Heureux', borderColor: '#22B14C', backgroundColor: '#52D17C', fill:true},
        ];

        this.chart?.update();
        console.log( this.barChartData.labels);
      }
    }, error =>{ console.error(error)}
    );
  }

  public barChartOptions: ChartConfiguration['options'] = { responsive: true  };
  public barChartType: ChartType = 'bar';

  public barChartData: ChartData<'line'> = { datasets: [ ] };

  // events
  public chartClicked({ event, active }: { event?: ChartEvent, active?: {}[] }): void {
    console.log(event, active);
  }

  public chartHovered({ event, active }: { event?: ChartEvent, active?: {}[] }): void {
    //console.log(event, active);
  }
}
