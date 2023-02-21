import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Mood, Device, MoodFace } from "../../models";
import { filter, map, tap } from 'rxjs';

@Component({
  selector: 'MoodsCreate',
  templateUrl: `moods-create.html`,
  styles: []
})
export class MoodsCreate implements OnInit {
  private httpClient: HttpClient;
  private activedRoute: ActivatedRoute;
  public mood: Mood = new Mood();
  public router: Router;
  public devices: Device[]|undefined;
  public moodFaces: MoodFace[]|undefined;
  public dateString :string="";

  constructor(router: Router, http: HttpClient, activedRoute: ActivatedRoute) {
    this.router = router;
    this.httpClient = http;
    this.activedRoute = activedRoute;
    
  }

  ngOnInit() {
    this.mood.moodDeviceId = 1;
    this.mood.moodFaceId = 1;

    var now =  new Date(Date.now());
    this.dateString =now.toISOString().substring(0, 10) +" "+ ("0" + now.getHours()).slice(-2) +":"+("0" + now.getMinutes()).slice(-2)+":00";

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

  submit() {
      this.httpClient.post(` http://localhost:7120/api/MoodEntries`, this.mood)
        .subscribe(result => this.router.navigate(['/surMaRoute']));
      }

    selectMoodFace(id:number){
        this.mood.moodFaceId= id;
    }

    selectDevice(id:number){
        this.mood.moodDeviceId= id;
    }
}