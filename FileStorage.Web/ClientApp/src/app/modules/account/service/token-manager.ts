import { Injectable } from '@angular/core';
import { TokenWrap } from '../model/token-wrap';
import { HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable()
export class TokenManager {
    private KEY = 'token';

    constructor(
        private router: Router
    ) { }
    save(token: TokenWrap) {
        localStorage.setItem(this.KEY, JSON.stringify(token));
    }

    get() {
        var tokenJSON = localStorage.getItem(this.KEY);
        var token = JSON.parse(tokenJSON) as TokenWrap;
        if (token == null || new Date(token.expireDate).getTime() < new Date().getTime()) {
            // this.router.navigate(['login']);
            return null;
        }
        return token;
    }

    remove() {
        localStorage.removeItem(this.KEY);
    }

    isAuthenticated(): boolean {
        return this.get() != null;
    }

    getHeaders(): HttpHeaders {
        var token = this.get();
        var headers = new HttpHeaders();
        return headers.set('Authorization', 'Bearer ' + token.token);
    }
}