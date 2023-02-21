import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Device } from './device';

@Component({
  selector: 'DevicesEdit',
  templateUrl: `devices-edit.html`,
  styles: []
})
export class DevicesEdit implements OnInit {
  public device: Device|undefined;

  constructor(private httpClient: HttpClient, private router: Router, private activedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    let id = this.activedRoute.snapshot.paramMap.get('id');
    console.log(id);
    this.httpClient.get<Device>(`http://localhost:7120/api/Devices/${id}`).subscribe(result => {
      this.device = result;
    }, error =>{ console.error(error)}
    );
  }

  delete() {
    if (this.device != undefined) {
        this.httpClient.delete(`http://localhost:7120/api/Devices/${this.device.id}`)
          .subscribe(result => this.router.navigate(['/DevicesList']));
      }
    }

    save() {
      if (this.device != undefined) {
        this.httpClient.put(`http://localhost:7120/api/Devices`, this.device)
          .subscribe(result => this.router.navigate(['/DevicesList']));
        }
      }
}