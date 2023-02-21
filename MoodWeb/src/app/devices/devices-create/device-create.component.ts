import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Device } from "../../models"

@Component({
  selector: 'mood-device-create',
  templateUrl: "device-create.component.html",
  styles: [
  ]
})
export class DeviceCreateComponent {
  private httpClient: HttpClient;
  public device: Device = new Device;
  public router: Router;

  @ViewChild('createDeviceForm') 
  createDeviceForm!: NgForm;

  constructor(router: Router, http: HttpClient) {
    this.router = router;
    this.httpClient = http;
  }

  save()
  {
    this.httpClient.post(`http://localhost:7120/api/Devices/`, this.device)
      .subscribe(result => this.router.navigate(['/DevicesList']))
  }
}