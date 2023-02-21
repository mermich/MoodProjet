 import { NgModule } from '@angular/core';
 import { BrowserModule } from '@angular/platform-browser';
 import { HttpClientModule } from '@angular/common/http';

 import { FormsModule } from '@angular/forms';

import { Parent } from './parent.component';
import { Child } from './child.component';
import { ChildData } from './childData.component';
import { Child2Way } from './child2Way.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    { path: 'parent', component: Parent },
  ];

 @NgModule({
   declarations: [
     Parent,
     Child,
     ChildData,
     Child2Way
   ],
   imports: [
      RouterModule.forRoot(routes),
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      HttpClientModule,
      FormsModule,
   ],
   providers: []
 })
 export class ParentChildDemoModule { }

 