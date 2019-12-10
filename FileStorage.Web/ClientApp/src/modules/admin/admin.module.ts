import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material.module';
import { RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { CreateCategoryComponent } from './category/create-category/create-category.component';
import { CategoryHomeComponent } from './category/category-home/category-home.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';

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
        EditCategoryComponent
    ],
    exports: [
        AdminComponent,
        CreateCategoryComponent,
        CategoryHomeComponent,
        EditCategoryComponent
    ]
})
export class AdminModule { }