import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import jwtDecode from "jwt-decode";
import { LoginResult, LoginToken, UserLogin } from "../models";
import { LoginResultService } from "./LoginResultService";

@Component({
    selector: 'login',
    templateUrl: `loginComponent.html`
})
export class LoginComponent {
    public creds: UserLogin = new UserLogin();

    constructor(private httpClient: HttpClient, private router: Router, private loginResultService: LoginResultService, private activatedRoute: ActivatedRoute) { }

    Login() {
        console.log(this.creds);
        this.httpClient.post<LoginResult>('http://localhost:7120/api/Login', this.creds).subscribe(result => {
            console.log(result);
            if (result.isLoginOK) {
                this.loginResultService.LoginTokenString = result.token;
                this.loginResultService.LoginToken = jwtDecode(result.token);
                console.log(jwtDecode(result.token));
                console.log(this.loginResultService.LoginToken.CanAdminDevices);

                let redirectTo = this.activatedRoute.snapshot.params['redirectTo'];
                if (redirectTo != undefined) {
                    this.router.navigate(["" + redirectTo]);
                }
                else {
                    this.router.navigate([""]);
                }
            }
            else {
                this.loginResultService.LoginTokenString = undefined;
                this.loginResultService.LoginToken = new LoginToken();
            }
        }, error => {
            this.loginResultService.LoginTokenString = undefined;
            this.loginResultService.LoginToken = new LoginToken();
            console.error(error)
        });
    }
}