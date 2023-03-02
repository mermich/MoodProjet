import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MoodFace } from "../../models";
import { LoginResultService } from 'src/app/auth/LoginResultService';

@Component({
  selector: 'MoodFacesList',
  templateUrl: `mood-faces-list.html`,
  styles: []
})
export class MoodFacesList implements OnInit {
  public moodFaces: MoodFace[] | undefined;

  constructor(private router: Router, private httpClient: HttpClient, private loginResultService: LoginResultService) { }

  ngOnInit() {
    console.log('ngOnInit');
    if (this.loginResultService.LoginToken.CanAdminMoodFaces) {
      // ok

      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.get<MoodFace[]>('http://localhost:7120/api/MoodFaces', { headers: finalHeader }).subscribe(result => {
        this.moodFaces = result;
      }, error => { console.error(error) }
      );

    }
    else {
      this.router.navigate(['/login', { redirectTo: 'MoodFacesList' }]);
    }
  }

  selectedMoodFace: number | undefined;
  selectMoodFace(id: number) {
    this.selectedMoodFace = id;
  }

  deleteSelectedMoodFace() {
    const headers = new HttpHeaders();
    var finalHeader = headers
      .append('Content-Type', 'application/json; charset=utf-8')
      .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);


    this.httpClient.delete<MoodFace[]>(`http://localhost:7120/api/MoodFaces/${this.selectedMoodFace}`, { headers: finalHeader }).subscribe(result => {
      this.moodFaces = result;
    }, error => { console.error(error) }
    );
  }
}