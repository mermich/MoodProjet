import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { LoginResultService } from '../auth/LoginResultService';
import { MoodByHoursChartData } from './mood';

@Component({
  selector: 'moodByHours',
  template: `<canvas baseChart class="chart" [data]="barChartData" [options]="barChartOptions" [type]="barChartType"></canvas>`
})
export class MoodByHourComponent implements OnInit {

  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  constructor(private httpClient: HttpClient, private loginResultService: LoginResultService) { }

  public moods: MoodByHoursChartData[] | undefined;

  ngOnInit() {
    console.log('ngOnInit');

    const headers = new HttpHeaders();
    var finalHeader = headers.append('Content-Type', 'application/json; charset=utf-8')
      .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

    this.httpClient.get<MoodByHoursChartData[]>('http://localhost:7120/api/Charts-GetMoodByHours', { headers: finalHeader }).subscribe(result => {
      this.moods = result;
      if (this.moods) {
        this.barChartData.labels = this.moods.map(e => e.heure);
        this.barChartData.datasets = [
          { data: this.moods.map(e => e.face4Count), label: 'Triste', borderColor: '#FF7F27', backgroundColor: '#FFAF57', fill: true },
          { data: this.moods.map(e => e.face3Count), label: 'Neutre', borderColor: '#EFE4B0', backgroundColor: '#FFF4E0', fill: true },
          { data: this.moods.map(e => e.face2Count), label: 'Content', borderColor: '#B5E61D', backgroundColor: '#E5F63D', fill: true },
          { data: this.moods.map(e => e.face1Count), label: 'Heureux', borderColor: '#22B14C', backgroundColor: '#52D17C', fill: true },
        ];

        this.chart?.update();
        console.log(this.barChartData.labels);
      }
    }, error => { console.error(error) }
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

  public barChartData: ChartData<'line'> = { datasets: [] };
}
