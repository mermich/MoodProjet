import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Device } from './device';
import { LoginResultService } from 'src/app/auth/LoginResultService';

@Component({
  selector: 'DevicesEdit',
  templateUrl: `devices-edit.html`,
  styles: []
})
export class DevicesEdit implements OnInit {
  public device: Device | undefined;

  constructor(private httpClient: HttpClient, private router: Router, private activedRoute: ActivatedRoute, private loginResultService: LoginResultService) { }

  ngOnInit() {
    if (this.loginResultService.LoginToken.CanAdminDevices) {
      let id = this.activedRoute.snapshot.paramMap.get('id');
      console.log(id);

      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.get<Device>(`http://localhost:7120/api/Devices/${id}`, { headers: finalHeader }).subscribe(result => {
        this.device = result;
      }, error => { console.error(error) }
      );
    }
    else {
      this.router.navigate(['/login', { redirectTo: 'DevicesList' }]);
    }
  }

  delete() {
    if (this.device != undefined) {
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.delete(`http://localhost:7120/api/Devices/${this.device.id}`, { headers: finalHeader })
        .subscribe(result => this.router.navigate(['/DevicesList']));
    }
  }

  save() {
    if (this.device != undefined) {
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.put(`http://localhost:7120/api/Devices`, this.device, { headers: finalHeader })
        .subscribe(result => this.router.navigate(['/DevicesList']));
    }
  }
}