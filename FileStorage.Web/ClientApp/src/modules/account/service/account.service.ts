import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterModel } from '../model/register.model';
import { TokenWrap } from '../model/token-wrap';
import { TokenManager } from './token-manager';
import { LoginModel } from '../model/login.model';
import { LiteEvent } from 'src/app/infrastructure/lite-event';
import { ChangeRoleModel } from '../model/change-role.model';
import { Observable } from 'rxjs';

@Injectable()
export class AccountService {
    private url = "/account/";
    private readonly onLoginComplete: LiteEvent<boolean> = new LiteEvent();
    private readonly onRegisterComplete: LiteEvent<boolean> = new LiteEvent();

    constructor(
        private httpClient: HttpClient,
    ) { }

    register(model: RegisterModel) : Observable<TokenWrap> {
        return this.httpClient.post<TokenWrap>(this.url + "register", model, { observe: 'body' });
    }

    login(model: LoginModel) {
        return this.httpClient.post<TokenWrap>(this.url + "login", model, { observe: 'body' });
    }

    makeAdmin(model: ChangeRoleModel) {
        this.httpClient.post(this.url + "MakeAdmin", model).subscribe();
    }

    makeUser(model: ChangeRoleModel) {
        this.httpClient.post(this.url + "makeUser", model).subscribe();
    }
}