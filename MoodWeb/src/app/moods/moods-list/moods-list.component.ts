import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Mood } from './mood';

@Component({
  selector: 'MoodsList',
  templateUrl: `moods-list.html`,
  styles: []
})
export class MoodsList implements OnInit {
  private httpClient: HttpClient;
  public moods: Mood[]|undefined;

  constructor(http: HttpClient, private activedRoute: ActivatedRoute) {
    this.httpClient = http;
  }

  ngOnInit() {
    console.log('ngOnInit');

    this.httpClient.get<Mood[]>('http://localhost:7120/api/MoodEntries?includes=moodFace,device').subscribe(result => {
      this.moods = result;
    }, error =>{ console.error(error)}
    );
  }

  selectedMood:number|undefined;
  
  selectMood(id:number)
  {
    this.selectedMood = id;
  }

  deleteSelectedMood()
  {
    if(this.selectedMood != undefined)
    {
      this.httpClient.delete(`http://localhost:7120/api/MoodEntries/${this.selectedMood}`, ).subscribe(result => {
      }, error =>{ console.error(error)}
      );
  }
}
}