import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MoodFace } from "../../models"
import { LoginResultService } from 'src/app/auth/LoginResultService';

@Component({
  selector: 'MoodFacesCreate',
  templateUrl: `mood-faces-create.html`,
  styles: []
})
export class MoodFacesCreate implements OnInit {
  public moodFace: MoodFace = new MoodFace();

  constructor(private httpClient: HttpClient, private activedRoute: ActivatedRoute, private router: Router, private loginResultService: LoginResultService) { }

  ngOnInit() {
    if (this.loginResultService.LoginToken.CanAdminMoodFaces) {
      // ok
    }
    else {
      this.router.navigate(['/login', { redirectTo: 'MoodFacesList' }]);
    }
  }

  save() {
    if (this.moodFace != undefined) {
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.post(`http://localhost:7120/api/MoodFaces`, this.moodFace, { headers: finalHeader })
        .subscribe(result => this.router.navigate(['/MoodFacesList']));
    }
  }
}