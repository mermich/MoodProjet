import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Device, Mood, MoodFace } from '../../models';
import { map, tap } from 'rxjs';
import { LoginResultService } from 'src/app/auth/LoginResultService';

@Component({
  selector: 'MoodsEdit',
  templateUrl: `moods-edit.html`,
  styles: []
})
export class MoodsEdit implements OnInit {
  public mood: Mood | undefined;
  public moodFaces: MoodFace[] | undefined;
  public devices: Device[] | undefined;

  constructor(private router: Router, private httpClient: HttpClient, private activedRoute: ActivatedRoute, private loginResultService: LoginResultService) { }

  ngOnInit() {
    if (this.loginResultService.LoginToken.CanAdminMoodEntries) {
      // ok
      let id = this.activedRoute.snapshot.paramMap.get('id');

      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.get<Mood>(`http://localhost:7120/api/MoodEntries/${id}`, { headers: finalHeader })
        .pipe(tap((x) => console.log(x)))
        .subscribe(result => {
          console.table(result);
          this.mood = result;
        });

      this.httpClient.get<Device[]>('http://localhost:7120/api/Devices', { headers: finalHeader })
        .pipe(tap((x) => console.log(x)))
        .pipe(map(sellers => sellers.filter(seller => seller.isActive))).subscribe(devices => {
          console.table(devices);
          this.devices = devices
        });

      this.httpClient.get<MoodFace[]>('http://localhost:7120/api/MoodFaces', { headers: finalHeader })
        .pipe(tap((x) => console.log(x)))
        .pipe(map(sellers => sellers.filter(seller => seller.isActive))).subscribe(moodFaces => {
          console.table(moodFaces);
          this.moodFaces = moodFaces
        });
    }
    else {
      this.router.navigate(['/login', { redirectTo: 'MoodsList' }]);
    }
  }

  delete() {
    if (this.mood != undefined) {
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

      this.httpClient.delete(` http://localhost:7120/api/MoodEntries/${this.mood.id}`, { headers: finalHeader })
        .subscribe(result => this.router.navigate(['/MoodsList']));
    }
  }

  save() {
    if (this.mood != undefined) {
      const headers = new HttpHeaders();
      var finalHeader = headers
        .append('Content-Type', 'application/json; charset=utf-8')
        .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);
      this.httpClient.put(` http://localhost:7120/api/MoodEntries`, this.mood, { headers: finalHeader })
        .subscribe(result => this.router.navigate(['/MoodsList']));
    }
  }


  selectMoodFace(id: number) {
    if (this.mood != undefined) {
      this.mood.moodFaceId = id;
    }
  }

  selectDevice(id: number) {
    if (this.mood != undefined) {
      this.mood.moodDeviceId = id;
    }
  }
}