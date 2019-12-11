import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from 'src/app/material.module';
import { RegisterComponent } from './register/register.component';


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        MaterialModule,
        ReactiveFormsModule
    ],
    declarations: [
        RegisterComponent,
        LoginComponent
    ],
    exports: [
        RegisterComponent,
        LoginComponent
    ],
})
export class AccountModule { }