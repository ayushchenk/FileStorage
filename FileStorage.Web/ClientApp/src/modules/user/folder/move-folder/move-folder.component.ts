import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { FolderHomeComponent } from '../folder-home/folder-home.component';
import { FolderService } from 'src/service/folder.service';
import { Folder } from 'src/model/folder';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateFolderComponent } from '../create-folder/create-folder.component';
import { NavFolderComponent } from '../nav-folder/nav-folder.component';

@Component({
    selector: 'app-move-folder',
    templateUrl: './move-folder.component.html',
    styleUrls: ['./move-folder.component.css']
})
export class MoveFolderComponent {
    // private allFolders: Folder[];
    // private folders: Folder[];
    // private path: Folder[] = [];
    // private currentFolder: Folder;
    private folderToMove: Folder;

    @ViewChild(NavFolderComponent, null)
    nav: NavFolderComponent;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<MoveFolderComponent>,
        private folderService: FolderService,
    ) {
        this.folderToMove = data.folderToMove;
        // this.allFolders = data.allFolders.filter(f => f.id != this.folderToMove.id);
        // this.folders = data.folders.filter(f => f.id != this.folderToMove.id);
        // this.path = data.path;
        // this.currentFolder = data.currentFolder;
    }

    // navFolder(folder: Folder) {
    //     this.currentFolder = folder;
    //     this.folders = this.allFolders.filter(f => f.parentFolderId == folder.id);
    //     if (!this.path.includes(folder)) {
    //         this.path.push(folder);
    //     } else {
    //         var index = this.path.indexOf(folder);
    //         var count = this.path.length - index - 1;
    //         while (count-- != 0) {
    //             this.path.pop();
    //         }
    //     }
    // }

    // navRoot() {
    //     while (this.path.length != 0) {
    //         this.path.pop();
    //     }
    //     this.currentFolder = new Folder(null, "", null, null);
    //     this.folders = this.allFolders.filter(f => f.parentFolderId == null);
    // }

    get disabled() : boolean{
        return this.folderToMove.parentFolderId == this.nav.getCurrentFolder.id;
    }

    cancel() {
        this.dialogRef.close(null);
    }

    move() {
        // this.folderToMove.parentFolderId = this.currentFolder.id;
        this.folderToMove.parentFolderId = this.nav.getCurrentFolder.id;
        this.folderService.put(this.folderToMove, true).subscribe(response => {
            if(response.ok){
                this.dialogRef.close(response.body);
            }
        });
    }
}
