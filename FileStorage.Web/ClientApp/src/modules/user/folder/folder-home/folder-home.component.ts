import { Component, OnInit, ViewChild, Output, EventEmitter } from "@angular/core";
import { FolderService } from 'src/service/folder.service';
import { Router, RouterEvent, NavigationEnd } from '@angular/router';
import { Folder } from 'src/model/folder';
import { filter } from 'rxjs/operators';
import { Guid } from 'guid-typescript';
import { MatDialog } from '@angular/material/dialog';
import { CreateFolderComponent } from '../create-folder/create-folder.component';
import { EditFolderComponent } from '../edit-folder/edit-folder.component';
import { MatMenuTrigger } from '@angular/material/menu';
import { CompileShallowModuleMetadata } from '@angular/compiler';
import { saveAs } from '@progress/kendo-file-saver';
import { MoveFolderComponent } from '../move-folder/move-folder.component';

@Component({
    selector: 'folder-home',
    templateUrl: './folder-home.component.html'
    // styleUrls: ['./folder-home.component.css'],
})
export class FolderHomeComponent implements OnInit {
    private displayedColumns = ['folderName', 'actions'];
    private currentFolder: Folder = new Folder(null, "", null, null);
    private allFolders: Folder[] = [];
    private folders: Folder[] = [];
    private path: Folder[] = [];

    @Output()
    currentFolderEvent = new EventEmitter<Folder>();

    @ViewChild(MatMenuTrigger, null)
    contextMenu: MatMenuTrigger;
    contextMenuPosition = { x: '0px', y: '0px' };

    constructor(
        private folderService: FolderService,
        private dialog: MatDialog
    ) { }

    ngOnInit() {
        this.folderService.getAll().subscribe(data => this.reset(data));
    }

    public get getCurrentFolder() {
        return this.currentFolder;
    }

    public get detAllFolders(){
        return this.allFolders;
    }

    public get getFolders(){
        return this.folders;
    }

    public get getPath(){
        return this.path;
    }

    deleteFolder(folder: Folder) {
        if (confirm(`Are you sure want to delete '${folder.folderName}'`)) {
            this.folderService.delete(folder.id).subscribe((response) => {
                if (response.ok) {
                    this.allFolders = this.allFolders.filter(f => f.id != folder.id);
                    this.folders = this.folders.filter(f => f.id != folder.id);
                }
            });
        }
    }

    newFolder() {
        this.dialog.open(CreateFolderComponent, {
            data: this.currentFolder.id
        }).afterClosed().subscribe((folder) => {
            if (folder != null) {
                this.allFolders.push(folder);
                this.folders.push(folder);
            }
        });
    }

    editFolder(folder: Folder) {
        this.dialog.open(EditFolderComponent, {
            data: folder
        });
    }

    downloadFolder(folder: Folder) {
        this.folderService.folder(folder.id).subscribe(data => {
            const blob = new Blob([data], { type: 'application/zip' });
            saveAs(blob, folder.folderName + ".zip");
        });
    }

    navFolder(folder: Folder) {
        this.currentFolder = folder;
        this.currentFolderEvent.emit(this.currentFolder);
        this.folders = this.allFolders.filter(f => f.parentFolderId == folder.id);
        if (!this.path.includes(folder)) {
            this.path.push(folder);
        } else {
            var index = this.path.indexOf(folder);
            var count = this.path.length - index - 1;
            while (count-- != 0) {
                this.path.pop();
            }
        }
    }

    moveFolder(folder: Folder) {
        this.dialog.open(MoveFolderComponent, {
            data: {
                folderToMove: folder,
                allFolders: this.allFolders.filter(f => f.id != folder.id),
                folders: this.folders.filter(f => f.id != folder.id),
                path: JSON.parse(JSON.stringify(this.path)),
                currentFolder: this.currentFolder
            }
        }).afterClosed().subscribe((data) => {
            if (data != null) {
                this.folders = this.folders.filter(f => f.id != folder.id);
                this.allFolders = this.allFolders.filter(f => f.id != folder.id);
                this.allFolders.push(data);
            }
        });
    }

    navRoot() {
        this.reset(this.allFolders);
    }

    reset(data: Folder[]) {
        while (this.path.length != 0) {
            this.path.pop();
        }
        this.currentFolder = new Folder(null, "", null, null);
        this.currentFolderEvent.emit(this.currentFolder);
        this.allFolders = data;
        this.folders = this.allFolders.filter(f => f.parentFolderId == null);
    }

    onContextMenu(event: MouseEvent, folder: Folder) {
        event.preventDefault();
        this.contextMenuPosition.x = event.clientX + 'px';
        this.contextMenuPosition.y = event.clientY + 'px';
        this.contextMenu.menuData = { 'folder': folder };
        this.contextMenu.openMenu();
    }

    disableContextMenu($event) {
        $event.preventDefault();
    }
}