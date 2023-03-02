import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginResultService } from '../auth/LoginResultService';


@Component({
  selector: 'charts',
  template: `<barchart></barchart><moodByHours></moodByHours>`
})
export class ChartsComponent implements OnInit {

  constructor(private httpClient: HttpClient, private router: Router, private loginResultService: LoginResultService) { }

  ngOnInit(): void {
    if (this.loginResultService.LoginToken.CanSeeCharts) {
      // ok.
    }
    else {
      this.router.navigate(['/login', { redirectTo: 'charts' }]);
    }
  }
}
