import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MoodFace } from "../../models";
import { LoginResultService } from 'src/app/auth/LoginResultService';


@Component({
  selector: 'MoodFacesEdit',
  templateUrl: `mood-faces-edit.html`,
  styles: []
})
export class MoodFacesEdit implements OnInit {
  public moodFace: MoodFace | undefined;

  constructor(private router: Router, private httpClient: HttpClient, private activedRoute: ActivatedRoute, private loginResultService: LoginResultService) { }

  ngOnInit() {
    if (this.loginResultService.LoginToken.CanAdminMoodFaces) {
      // ok
      console.log('ngOnInit');
      let id = this.activedRoute.snapshot.paramMap.get('id');
      console.log(id);

      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.get<MoodFace>(`http://localhost:7120/api/MoodFaces/${id}`, { headers: finalHeader }).subscribe(result => {
        this.moodFace = result;
      }, error => { console.error(error) }
      );
    }
    else {
      this.router.navigate(['/login', { redirectTo: 'MoodFacesList' }]);
    }
  }

  delete() {
    if (this.moodFace != undefined) {

      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.delete(`http://localhost:7120/api/MoodFaces/${this.moodFace.id}`, { headers: finalHeader })
        .subscribe(result => this.router.navigate(['/MoodFacesList']));
    }
  }

  save() {
    if (this.moodFace != undefined) {
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.put(`http://localhost:7120/api/MoodFaces`, this.moodFace, { headers: finalHeader })
        .subscribe(result => this.router.navigate(['/MoodFacesList']));
    }
  }
}