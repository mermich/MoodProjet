import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Device } from './device';
import { Modal } from 'materialize-css';

@Component({
  selector: 'DevicesList',
  templateUrl: `devices-list.html`,
  styles: []
})
export class DevicesList implements OnInit {
  private httpClient: HttpClient;
  public devices: Device[]|undefined;  

  constructor(http: HttpClient, private activedRoute: ActivatedRoute) {
    this.httpClient = http;
  }

  ngOnInit() {
    console.log('ngOnInit');
    this.httpClient.get<Device[]>('http://localhost:7120/api/Devices').subscribe(result => {
      this.devices = result;
    }, error =>{ console.error(error)}
    );
  }
}

export { Device };
