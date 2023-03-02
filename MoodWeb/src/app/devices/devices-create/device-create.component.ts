import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginResultService } from 'src/app/auth/LoginResultService';
import { Device } from "../../models"

@Component({
  selector: 'mood-device-create',
  templateUrl: "device-create.component.html",
  styles: [
  ]
})
export class DeviceCreateComponent implements OnInit {
  public device: Device = new Device;

  @ViewChild('createDeviceForm')
  createDeviceForm!: NgForm;

  constructor(private httpClient: HttpClient, private router: Router, private loginResultService: LoginResultService) { }

  ngOnInit(): void {
    if (this.loginResultService.LoginToken.CanAdminDevices) {
      // ok.
    }
    else {
      this.router.navigate(['/login', { redirectTo: 'DevicesList' }]);
    }
  }

  save() {
    const headers = new HttpHeaders();
    var finalHeader = headers
      .append('Content-Type', 'application/json; charset=utf-8')
      .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

    this.httpClient.post(`http://localhost:7120/api/Devices/`, this.device, { headers: finalHeader })
      .subscribe(result => this.router.navigate(['/DevicesList']))
  }
}