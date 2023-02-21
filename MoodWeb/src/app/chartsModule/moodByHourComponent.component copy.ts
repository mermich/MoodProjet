import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { MoodByHoursChartData } from './mood';


@Component({
  selector: 'moodByHours',
  template: `<canvas baseChart class="chart" data="barChartData" type="barChartType"></canvas>`
})
export class moodByHourComponent implements OnInit {

  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  private httpClient: HttpClient;

  constructor(http: HttpClient) {
    this.httpClient = http;
  }

  public moods : MoodByHoursChartData[]| undefined;

  ngOnInit() {
    // pipe est la methode de manipulation des Obervable.
    this.httpClient.get<MoodByHoursChartData[]>('http://localhost:7120/api/Charts-GetMoodByHours').subscribe(result => {
      this.moods = result;
      if( this.moods )
      {
        this.barChartData.labels= this.moods.map(e=>e.heure);
        this.barChartData.datasets=[
          { data: this.moods.map(e=>e.face1Count), label: 'Heureux', borderColor: '#22B14C', backgroundColor: '#22B14C'},
          { data: this.moods.map(e=>e.face2Count), label: 'Content', borderColor: '#B5E61D', backgroundColor: '#B5E61D'},
          { data: this.moods.map(e=>e.face3Count), label: 'Neutre', borderColor: '#EFE4B0', backgroundColor: '#EFE4B0'},
          { data: this.moods.map(e=>e.face4Count), label: 'Triste', borderColor: '#FF7F27', backgroundColor: '#FF7F27'},
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
  public barChartType: ChartType = 'line';

  public barChartData: ChartData<'line'> = {
    datasets: [
      { data: [ 65, 59, 80, 81, 56, 55, 40 ], label: 'Series A' },
      { data: [ 28, 48, 40, 19, 86, 27, 90 ], label: 'Series B' }
    ]
  };
}
