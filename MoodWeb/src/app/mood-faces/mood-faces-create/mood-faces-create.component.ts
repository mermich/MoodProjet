import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MoodFace } from "../../models"

@Component({
  selector: 'MoodFacesCreate',
  templateUrl: `mood-faces-create.html`,
  styles: []
})
export class MoodFacesCreate implements OnInit {
  private httpClient: HttpClient;
  private activedRoute: ActivatedRoute;
  public moodFace: MoodFace = new MoodFace();
  public router: Router;

  constructor(router: Router, http: HttpClient, activedRoute: ActivatedRoute) {
    this.router = router;
    this.httpClient = http;
    this.activedRoute = activedRoute;
  }

  ngOnInit() {
  }

    save() {
      if (this.moodFace != undefined) {
        this.httpClient.post(`http://localhost:7120/api/MoodFaces`, this.moodFace)
          .subscribe(result => this.router.navigate(['/surMaRoute']));
        }
      }
}