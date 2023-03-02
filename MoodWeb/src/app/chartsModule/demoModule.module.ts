import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BarchartDemoComponent } from './barchartDemoComponent.component';
import { NgChartsModule } from 'ng2-charts';
import { MoodByHourComponent } from './moodByHourComponent.component';
import { ChartsComponent } from './chartsComponent.component';


const routes: Routes = [
  { path: 'barchart', component: BarchartDemoComponent },
  { path: 'moodByHours', component: MoodByHourComponent },
  { path: 'charts', component: ChartsComponent }
];

@NgModule({
  declarations: [BarchartDemoComponent, MoodByHourComponent, ChartsComponent],
  imports: [
    RouterModule.forRoot(routes),
    NgChartsModule
  ],
  providers: []
})
export class ChartsModule { }
