import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MoodFace } from "../../models";


@Component({
  selector: 'MoodFacesEdit',
  templateUrl: `mood-faces-edit.html`,
  styles: []
})
export class MoodFacesEdit implements OnInit {
  private httpClient: HttpClient;
  private activedRoute: ActivatedRoute;
  public moodFace: MoodFace|undefined;
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
    this.httpClient.get<MoodFace>(`http://localhost:7120/api/MoodFaces/${id}`).subscribe(result => {
      this.moodFace = result;
    }, error =>{ console.error(error)}
    );
  }

  delete() {
    if (this.moodFace != undefined) {
        this.httpClient.delete(`http://localhost:7120/api/MoodFaces/${this.moodFace.id}`)
          .subscribe(result => this.router.navigate(['/MoodFacesList']));
      }
    }

    save() {
      if (this.moodFace != undefined) {
        this.httpClient.put(`http://localhost:7120/api/MoodFaces`, this.moodFace)
          .subscribe(result => this.router.navigate(['/MoodFacesList']));
        }
      }
}