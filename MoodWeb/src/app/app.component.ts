import { Component } from '@angular/core';

@Component({
  selector: 'mood-root',
  template: `
    <app-nav-menu></app-nav-menu>
    <div style="text-align:center" class="content" style="max-width: 800px;margin: auto;">
    <router-outlet></router-outlet></div>`,
  styles: []
})
export class AppComponent {
  title = 'MoodWeb';
}
