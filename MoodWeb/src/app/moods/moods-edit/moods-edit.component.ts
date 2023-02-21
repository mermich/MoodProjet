import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Device, Mood, MoodFace } from '../../models';
import { map, tap } from 'rxjs';

@Component({
  selector: 'MoodsEdit',
  templateUrl: `moods-edit.html`,
  styles: []
})
export class MoodsEdit implements OnInit {
  private httpClient: HttpClient;
  private activedRoute: ActivatedRoute;
  public mood: Mood|undefined;
  public moodFaces: MoodFace[]|undefined;
  public devices: Device[]|undefined;
  

  public router: Router;

  constructor(router: Router, http: HttpClient, activedRoute: ActivatedRoute) {
    this.router = router;
    this.httpClient = http;
    this.activedRoute = activedRoute;
  }

  ngOnInit() {
    let id = this.activedRoute.snapshot.paramMap.get('id');
    this.httpClient.get<Mood>(`http://localhost:7120/api/MoodEntries/${id}`)
    .pipe(tap((x) => console.log(x)))
    .subscribe(result => {
      console.table(result);
      this.mood = result;
    });

    this.httpClient.get<Device[]>('http://localhost:7120/api/Devices')
    .pipe(tap((x) => console.log(x)))
    .pipe(map(sellers => sellers.filter(seller => seller.isActive))).subscribe(devices=>
    {
      console.table(devices);
      this.devices = devices
    });


    this.httpClient.get<MoodFace[]>('http://localhost:7120/api/MoodFaces')
    .pipe(tap((x) => console.log(x)))
    .pipe(map(sellers => sellers.filter(seller => seller.isActive))).subscribe(moodFaces=>{
      console.table(moodFaces);
      this.moodFaces = moodFaces
      });
  }

  delete() {
    if (this.mood != undefined) {
        this.httpClient.delete(` http://localhost:7120/api/MoodEntries/${this.mood.id}`)
          .subscribe(result => this.router.navigate(['/MoodsList']));
      }
    }

    save() {
      if (this.mood != undefined) {
        this.httpClient.put(` http://localhost:7120/api/MoodEntries`, this.mood)
          .subscribe(result => this.router.navigate(['/MoodsList']));
      }
    }


    selectMoodFace(id:number){
      if(this.mood != undefined)
      {
        this.mood.moodFaceId= id;
      }
    }

    selectDevice(id:number){
      if(this.mood != undefined)
      {
        this.mood.moodDeviceId= id;
      }
    }
}
