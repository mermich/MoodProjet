import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Device, Mood, MoodFace } from '../../models';
import { LoginResultService } from 'src/app/auth/LoginResultService';

@Component({
  selector: 'MoodsList',
  templateUrl: `moods-list.html`,
  styles: []
})
export class MoodsList implements OnInit {
  public moods: Mood[] | undefined;

  constructor(private httpClient: HttpClient, private activedRoute: ActivatedRoute, private router: Router, private loginResultService: LoginResultService) { }

  ngOnInit() {
    console.log('ngOnInit');
    if (this.loginResultService.LoginToken.CanAdminMoodEntries) {
      // ok
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.get<Mood[]>('http://localhost:7120/api/MoodEntries?includes=moodFace,device', { headers: finalHeader }).subscribe(result => {
        this.moods = result;
      }, error => { console.error(error) });
    }
    else {
      this.router.navigate(['/login', { redirectTo: 'MoodsList' }]);
    }
  }

  selectedMood: number | undefined;

  selectMood(id: number) {
    this.selectedMood = id;
  }

  deleteSelectedMood() {
    if (this.selectedMood != undefined) {
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.delete(`http://localhost:7120/api/MoodEntries/${this.selectedMood}`, { headers: finalHeader }).subscribe(result => {
        this.ngOnInit();
      }, error => { console.error(error) }
      );
    }
  }
}