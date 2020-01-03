import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material.module';
import { RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { CreateCategoryComponent } from './category/create-category/create-category.component';
import { CategoryHomeComponent } from './category/category-home/category-home.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserService } from 'src/app/service/user.service';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        RouterModule,
    ],
    declarations: [
        AdminComponent,
        CreateCategoryComponent,
        CategoryHomeComponent,
        EditCategoryComponent,
        UserListComponent
    ],
    exports: [
        AdminComponent,
        CreateCategoryComponent,
        CategoryHomeComponent,
        EditCategoryComponent,
        UserListComponent
    ],
    providers : [UserService]
})
export class AdminModule { }