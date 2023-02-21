import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BarchartDemoComponent } from './barchartDemoComponent.component';
import { NgChartsModule } from 'ng2-charts';
import { moodByHourComponent } from './MoodByHourComponent.component copy';

const routes: Routes = [
    { path: 'barchart', component: BarchartDemoComponent },
    { path: 'moodByHours', component: moodByHourComponent },
  ];

 @NgModule({
   declarations: [ BarchartDemoComponent ],
   imports: [
      RouterModule.forRoot(routes),
      NgChartsModule
   ],
   providers: []
 })
 export class ChartsModule { }
