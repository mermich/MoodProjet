import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DevicesList } from './devices/devices-list/devices-list.component';
import { DevicesEdit } from './devices/devices-edit/devices-edit.component';
import { DeviceCreateComponent } from './devices/devices-create/device-create.component';

import { MoodsList } from './moods/moods-list/moods-list.component';
import { MoodsEdit } from './moods/moods-edit/moods-edit.component';
import { MoodsCreate } from './moods/moods-create/moods-create.component';

import { MoodFacesList } from './mood-faces/mood-faces-list/mood-faces-list.component';
import { MoodFacesEdit } from './mood-faces/mood-faces-edit/mood-faces-edit.component';
import { MoodFacesCreate } from './mood-faces/mood-faces-create/mood-faces-create.component';

const routes: Routes = [
  { path: 'DevicesList', component: DevicesList },
  { path: 'DevicesEdit/:id', component: DevicesEdit },
  { path: 'DevicesCreate', component: DeviceCreateComponent },

  { path: 'MoodsList', component: MoodsList },
  { path: 'MoodsEdit/:id', component: MoodsEdit },
  { path: 'MoodsCreate', component: MoodsCreate },

  { path: 'MoodFacesList', component: MoodFacesList },
  { path: 'MoodFacesEdit/:id', component: MoodFacesEdit },
  { path: 'MoodFacesCreate', component: MoodFacesCreate }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
