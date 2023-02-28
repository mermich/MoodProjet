import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import jwtDecode from "jwt-decode";
import { LoginResult, UserLogin } from "../models";
import { LoginResultService } from "./LoginResultService";

@Component({
  selector: 'login',
  template: `
    <div class="card" style="margin: auto; margin-top: 50px;">
    <div class="card-content">
        <span class="card-title"><i class="material-icons left">login</i>Login</span>
<br><br>
        <div class="input-field">
            <input name="Login" [(ngModel)]="creds.Login"/>
            <label for="Login" class="active">Login</label>
        </div>

        <div class="input-field">
            <input name="Password" [(ngModel)]="creds.Password"/>
            <label for="Password" class="active">Password</label>
        </div>
    </div>

    <div class="card-action">
        <a class="waves-effect waves-light btn" type="submit" (click)="Login()" style="margin-right: 20px;" ><i class="material-icons right">save</i>Login</a>
    </div>
    </div>`})
export class LoginComponent
{    
    public creds:UserLogin= new UserLogin();

    constructor(private httpClient: HttpClient,private loginResultService :LoginResultService) {}

    Login()
    {     
        console.log( this.creds);
        this.httpClient.post<LoginResult>('http://localhost:7120/api/Login', this.creds).subscribe(result => {
        console.log(result);
        if(result.isLoginOK)
        {
            this.loginResultService.LoginTokenString = result.token;
            this.loginResultService.LoginToken = jwtDecode(result.token);
            console.log(jwtDecode(result.token));
            console.log(this.loginResultService.LoginToken?.CanAdminDevices);
        }
        else{
            this.loginResultService.LoginTokenString = undefined;
            this.loginResultService.LoginToken = undefined;
        }
        }, error =>{ 
            this.loginResultService.LoginTokenString = undefined;
            this.loginResultService.LoginToken = undefined;
            console.error(error)});
    }
}