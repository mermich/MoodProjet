import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { MoodFace } from "../../models";

@Component({
  selector: 'MoodFacesList',
  templateUrl: `mood-faces-list.html`,
  styles: []
})
export class MoodFacesList implements OnInit {
  private httpClient: HttpClient;
  public moodFaces: MoodFace[]|undefined;

  constructor(http: HttpClient, private activedRoute: ActivatedRoute) {
    this.httpClient = http;
  }

  ngOnInit() {
    console.log('ngOnInit');
    this.httpClient.get<MoodFace[]>('http://localhost:7120/api/MoodFaces').subscribe(result => {
      this.moodFaces = result;
    }, error =>{ console.error(error)}
    );
  }

  selectedMoodFace:number|undefined;
  selectMoodFace(id :number)
  {
    this.selectedMoodFace = id;
  }

  deleteSelectedMoodFace()
  {
    this.httpClient.delete<MoodFace[]>(`http://localhost:7120/api/MoodFaces/${this.selectedMoodFace}`).subscribe(result => {
      this.moodFaces = result;
    }, error =>{ console.error(error)}
    );
  }
}