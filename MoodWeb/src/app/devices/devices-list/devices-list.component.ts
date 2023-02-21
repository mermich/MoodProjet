import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Modal } from 'materialize-css';
import { filter, map, Observable, tap } from 'rxjs';
import { Device } from "../../models";


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

    // pipe est la methode de manipulation des Obervable.
    var res = this.httpClient.get<Device[]>('http://localhost:7120/api/Devices')
      .pipe(tap((x) => console.log(x)))
      .subscribe(devices => this.devices = devices);
  }
}