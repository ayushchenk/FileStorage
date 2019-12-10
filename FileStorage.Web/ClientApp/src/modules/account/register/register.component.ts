import { Component } from '@angular/core'
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';
import { AccountService } from '../service/account.service';
import { TokenManager } from '../service/token-manager';
import { RegisterModel } from '../model/register.model';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css'],
    providers: [AccountService, TokenManager],
})
export class RegisterComponent {
    registerModel: RegisterModel = new RegisterModel('', '');
    confirmPassword: string = '';

    constructor(
        private accountService: AccountService,
        private tokenManager: TokenManager,
        private snackBar: MatSnackBar,
        private router: Router
    ) { }

    submit() {
        this.accountService.register(this.registerModel).subscribe(
            data => {
                this.tokenManager.save(data);
                this.router.navigate(['']);
            },
            (error: HttpErrorResponse) => {
                this.snackBar.open(error.error.description, "OK", { duration: 3500 });
            }
        );
    }
}