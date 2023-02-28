import { Injectable } from "@angular/core";
import { LoginToken } from "../models";

@Injectable({
  providedIn: 'root',
 })
export class LoginResultService {
  public LoginTokenString: string|undefined;
  public LoginToken: LoginToken|undefined;
}