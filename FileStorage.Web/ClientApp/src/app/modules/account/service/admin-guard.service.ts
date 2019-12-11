import { Injectable } from '@angular/core';
import { Router, CanActivate, CanLoad, CanActivateChild } from '@angular/router';
import { TokenManager } from './token-manager';

@Injectable()
export class AdminGuardService implements CanActivate{
    constructor(
        private tokenManager: TokenManager,
        private router: Router
    ) { }
    canActivate(): boolean {
        if (!this.tokenManager.isAuthenticated()) {
            this.router.navigate(['login']);
            return false;
        }
        return this.tokenManager.get().isAdmin;
    }
}