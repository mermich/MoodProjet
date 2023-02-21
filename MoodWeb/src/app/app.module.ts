import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';

import { DevicesList } from './devices/devices-list/devices-list.component';
import { DevicesEdit } from './devices/devices-edit/devices-edit.component';
import { DeviceCreateComponent } from './devices/devices-create/device-create.component'

import { MoodsList } from './moods/moods-list/moods-list.component';
import { MoodsEdit } from './moods/moods-edit/moods-edit.component';
import { MoodsCreate } from './moods/moods-create/moods-create.component';

import { MoodFacesList } from './mood-faces/mood-faces-list/mood-faces-list.component';
import { MoodFacesEdit } from './mood-faces/mood-faces-edit/mood-faces-edit.component';
import { MoodFacesCreate } from './mood-faces/mood-faces-create/mood-faces-create.component';

import { NavMenuComponent } from './helpers/nav-menu/NavMenu.component';
import { ParentChildDemoModule } from './demos/parentChildDemo/parentChildDemo.module';
import { DemoModule } from './demos/demoModule/demoModule.module';

import { NgChartsModule } from 'ng2-charts';
import { ChartsModule } from './chartsModule/demoModule.module';
import { OuiNon } from './helpers/ouiNon';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    
    DevicesList,
    DevicesEdit,
    DeviceCreateComponent,

    MoodsList,
    MoodsEdit,
    MoodsCreate,

    MoodFacesList,
    MoodFacesEdit,
    MoodFacesCreate,
    OuiNon
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,    
    ParentChildDemoModule,
    DemoModule,
    ChartsModule,
    NgChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
