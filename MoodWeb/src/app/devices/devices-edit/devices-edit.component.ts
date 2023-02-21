import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Device } from "../../models"

@Component({
  selector: 'DevicesEdit',
  templateUrl: `devices-edit.html`,
  styles: []
})
export class DevicesEdit implements OnInit {
  private httpClient: HttpClient;
  private activedRoute: ActivatedRoute;
  public device: Device|undefined;
  public router: Router;

  constructor(router: Router, http: HttpClient, activedRoute: ActivatedRoute) {
    this.router = router;
    this.httpClient = http;
    this.activedRoute = activedRoute;
  }

  ngOnInit() {
    let id = this.activedRoute.snapshot.paramMap.get('id');
    console.log('ngOnInit');
    console.log(id);
    this.httpClient.get<Device>(`http://localhost:7120/api/Devices/${id}`).subscribe(result => {
      this.device = result;
    }, error =>{ console.error(error)}
    );
  }

  save() {
    if (this.device != undefined) {
      this.httpClient.put(`http://localhost:7120/api/Devices`, this.device)
        .subscribe(result => this.router.navigate(['/DevicesList']));
      }
    }
}