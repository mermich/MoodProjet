import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Mood, Device, MoodFace } from "../../models";
import { filter, map, tap } from 'rxjs';
import { LoginResultService } from 'src/app/auth/LoginResultService';

@Component({
  selector: 'MoodsCreate',
  templateUrl: `moods-create.html`,
  styles: []
})
export class MoodsCreate implements OnInit {
  public mood: Mood = new Mood();
  public devices: Device[] | undefined;
  public moodFaces: MoodFace[] | undefined;
  public dateString: string = "";

  constructor(private router: Router, private httpClient: HttpClient, private activedRoute: ActivatedRoute, private loginResultService: LoginResultService) { }

  ngOnInit() {
    this.mood.moodDeviceId = 1;
    this.mood.moodFaceId = 1;

    const headers = new HttpHeaders();
    var finalHeader = headers
      .append('Content-Type', 'application/json; charset=utf-8')
      .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

    var now = new Date(Date.now());
    this.dateString = now.toISOString().substring(0, 10) + " " + ("0" + now.getHours()).slice(-2) + ":" + ("0" + now.getMinutes()).slice(-2) + ":00";

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

  save() {
    const headers = new HttpHeaders();
    var finalHeader = headers
      .append('Content-Type', 'application/json; charset=utf-8')
      .append('Authorization', `Bearer ${this.loginResultService.LoginTokenString}`);

    this.httpClient.post(` http://localhost:7120/api/MoodEntries`, this.mood, { headers: finalHeader })
      .subscribe(result => this.router.navigate(['/surMaRoute']));
  }

  selectMoodFace(id: number) {
    this.mood.moodFaceId = id;
  }

  selectDevice(id: number) {
    this.mood.moodDeviceId = id;
  }
}