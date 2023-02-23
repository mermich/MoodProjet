import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { MoodByHoursChartData } from './mood';

@Component({
  selector: 'moodByHours',
  template: `<canvas baseChart class="chart" [data]="barChartData" [options]="barChartOptions" [type]="barChartType"></canvas>`
})
export class MoodByHourComponent implements OnInit {

  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  private httpClient: HttpClient;

  constructor(http: HttpClient) {
    this.httpClient = http;
  }

  public moods : MoodByHoursChartData[]| undefined;

  ngOnInit() {
    console.log('ngOnInit');

    this.httpClient.get<MoodByHoursChartData[]>('http://localhost:7120/api/Charts-GetMoodByHours').subscribe(result => {
      this.moods = result;
      if( this.moods )
      {
        // todo set labels.

        // todo set series.

        this.chart?.update();
        console.log( this.barChartData.labels);
      }
    }, error =>{ console.error(error)}
    );
  }

  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    scales: {
      y: {
        stacked: true
      }
    }

  };
  public barChartType: ChartType = 'line';

  public barChartData: ChartData<'line'> = {datasets: []};
}
