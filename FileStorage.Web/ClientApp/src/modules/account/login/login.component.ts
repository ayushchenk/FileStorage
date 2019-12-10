import { Component } from '@angular/core'
import { LoginModel } from '../model/login.model';
import { AccountService } from '../service/account.service';
import { TokenManager } from '../service/token-manager';
import { Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    providers: [AccountService, TokenManager],
})
export class LoginComponent {
    private loginModel: LoginModel = new LoginModel("", "");

    constructor(
        private accountService: AccountService,
        private tokenManager: TokenManager,
        private snackBar: MatSnackBar,
        private router: Router,
    ) { }

    submit() {
        this.accountService.login(this.loginModel).subscribe(
            data => {
                this.tokenManager.save(data);
                this.router.navigate(['']);
            },
            (error: HttpErrorResponse) => {
                this.snackBar.open(error.error, "OK", { duration: 3500 });
            }
        );
    }
}