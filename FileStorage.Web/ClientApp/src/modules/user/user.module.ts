import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material.module';
import { RouterModule } from '@angular/router';
import { UserComponent } from './user.component';
import { FolderService } from 'src/service/folder.service';
import { FileHomeComponent } from './file/file-home/file-home.component';
import { CreateFolderComponent } from './folder/create-folder/create-folder.component';
import { EditFolderComponent } from './folder/edit-folder/edit-folder.component';
import { FolderHomeComponent } from './folder/folder-home/folder-home.component';
import { CreateFileComponent } from './file/create-file/create-file.component';
import { FileService } from 'src/service/file.service';
import { CategoryService } from 'src/service/category.service';
import { EditFileComponent } from './file/edit-file/edit-file.component';
import { MoveFolderComponent } from './folder/move-folder/move-folder.component';
import { NavFolderComponent } from './folder/nav-folder/nav-folder.component';
import { MoveFileComponent } from './file/move-file/move-file.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        RouterModule,
    ],
    declarations: [
        UserComponent,
        FileHomeComponent,
        CreateFolderComponent,
        EditFolderComponent,
        FolderHomeComponent,
        CreateFileComponent,
        EditFileComponent,
        MoveFolderComponent,
        NavFolderComponent,
        MoveFileComponent,
    ],
    exports: [
        UserComponent,
        FileHomeComponent,
        CreateFolderComponent,
        EditFolderComponent,
        FolderHomeComponent,
        CreateFileComponent,
        EditFileComponent,
        MoveFolderComponent,
        NavFolderComponent,
        MoveFileComponent,

    ],
    entryComponents:[
        CreateFolderComponent,
        EditFolderComponent,
        CreateFileComponent,
        EditFileComponent,
        MoveFolderComponent,
        MoveFileComponent,
    ],
    providers:[
        FolderService,
        FileService,
        CategoryService
    ]
})
export class UserModule { }