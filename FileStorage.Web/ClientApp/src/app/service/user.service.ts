import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { User } from '../model/user';
import { Observable } from 'rxjs';
import { TokenManager } from '../modules/account/service/token-manager';
import { ChangeRoleModel } from '../modules/account/model/change-role.model';

@Injectable()
export class UserService {
    private url: string = "/api/user";
    private roleUrl: string = "/account";

    constructor(
        private httpClient: HttpClient,
        private tokenManager: TokenManager
    ) { }

    getAll(): Observable<User[]> {
        return this.httpClient.get<User[]>(this.url, { headers: this.tokenManager.getHeaders() });
    }

    delete(id: string): Observable<HttpResponse<any>> {
        return this.httpClient.delete(this.url + "/" + id, { headers: this.tokenManager.getHeaders(), observe: "response" });
    }

    makeAdmin(email: string): Observable<HttpResponse<any>> {
        return this.httpClient.post(this.roleUrl + "/makeAdmin", new ChangeRoleModel(email), { headers: this.tokenManager.getHeaders(), observe: 'response' });
    }

    makeUser(email: string): Observable<HttpResponse<any>> {
        return this.httpClient.post(this.roleUrl + "/makeUser", new ChangeRoleModel(email), { headers: this.tokenManager.getHeaders(), observe: 'response' });
    }
}
