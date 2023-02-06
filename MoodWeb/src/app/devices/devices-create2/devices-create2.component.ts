import { HttpClient } from '@angular/common/http';
import { Component, ViewChild, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'mood-device-create',
  templateUrl: "devices-create2.html"
})
export class DeviceCreate2Component {
  private httpClient: HttpClient;
  
  public devices: Device[] | undefined;
  public device: Device = new Device;
  public router: Router;

  @ViewChild('createDeviceForm') 
  createDeviceForm!: NgForm;

  constructor(router: Router, http: HttpClient) {
    this.router = router;
    this.httpClient = http;
  }


  create(): void {
    console.log(this.device.label);
    console.log(this.createDeviceForm.form.valid);
    this.httpClient.post(`http://localhost:7120/api/Devices/`, this.device).subscribe(
      result => this.router.navigate(['/devices']))

  }
}

export class Device
{
  public label: string = "";
  public isActive: boolean = true;
}
