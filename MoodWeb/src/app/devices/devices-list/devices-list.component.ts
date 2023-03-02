import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Modal } from 'materialize-css';
import { filter, map, Observable, tap } from 'rxjs';
import { Device } from "../../models";
import { LoginResultService } from 'src/app/auth/LoginResultService';


@Component({
  selector: 'DevicesList',
  templateUrl: `devices-list.html`,
  styles: []
})
export class DevicesList implements OnInit {
  private httpClient: HttpClient;
  public devices: Device[] | undefined;

  constructor(http: HttpClient, private router: Router, private loginResultService: LoginResultService) {
    this.httpClient = http;
  }

  ngOnInit() {
    console.log('ngOnInit');
    if (this.loginResultService.LoginToken.CanAdminDevices) {
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      var res = this.httpClient.get<Device[]>('http://localhost:7120/api/Devices', { headers: finalHeader })
        .pipe(tap((x) => console.log(x)))
        .subscribe(devices => this.devices = devices);
    }
    else {
      this.router.navigate(['/login', { redirectTo: 'DevicesList' }]);
    }
  }
}