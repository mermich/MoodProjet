import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BarchartDemoComponent } from './barchartDemoComponent.component';
import { NgChartsModule } from 'ng2-charts';

const routes: Routes = [
    { path: 'barchart', component: BarchartDemoComponent },
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
