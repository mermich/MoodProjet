import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Mood } from './mood';
import { Device } from '../moods-edit/mood';

@Component({
  selector: 'MoodsCreate',
  templateUrl: `moods-create.html`,
  styles: []
})
export class MoodsCreate implements OnInit {
  private httpClient: HttpClient;
  private activedRoute: ActivatedRoute;
  public mood: Mood|undefined;
  public router: Router;
  public devices: Device[]|undefined;

  constructor(router: Router, http: HttpClient, activedRoute: ActivatedRoute) {
    this.router = router;
    this.httpClient = http;
    this.activedRoute = activedRoute;
  }

  ngOnInit() {
    let id = this.activedRoute.snapshot.paramMap.get('id');
    console.log('ngOnInit');
    console.log(id);
    this.httpClient.get<Mood>(`http://localhost:7120/api/MoodEntries/${id}`).subscribe(result => {
      this.mood = result;
    }, error =>{ console.error(error)}
    );
  }

  submit() {
      if (this.mood != undefined) {
        this.httpClient.post(`http://localhost:7120/api/Moods`, this.mood)
          .subscribe(result => this.router.navigate(['/surMaRoute']));
        }
      }
}