import { NgModule } from '@angular/core';
import { HelloComponent } from './hello.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'hello', component: HelloComponent },
];

@NgModule({
  declarations: [HelloComponent],
  imports: [
    RouterModule.forRoot(routes)
  ],
  providers: []
})
export class DemoModule { }
