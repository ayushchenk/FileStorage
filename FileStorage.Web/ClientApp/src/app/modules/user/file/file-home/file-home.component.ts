import { Component, OnInit, ViewChild, AfterViewInit } from "@angular/core";
import { FolderHomeComponent } from '../../folder/folder-home/folder-home.component';
import { FileService } from 'src/app/service/file.service';
import { CategoryService } from 'src/app/service/category.service';
import { MatDialog, throwMatDialogContentAlreadyAttachedError } from '@angular/material/dialog';
import { CreateFileComponent } from '../create-file/create-file.component';
import { MatMenuTrigger } from '@angular/material/menu';
import { EditFileComponent } from '../edit-file/edit-file.component';
import { saveAs, encodeBase64 } from '@progress/kendo-file-saver';
import { MoveFileComponent } from '../move-file/move-file.component';
import { Folder } from 'src/app/model/folder';
import { Category } from 'src/app/model/category';
import { FileModel } from 'src/app/model/file';

@Component({
    selector: 'file-home',
    templateUrl: './file-home.component.html',
    styleUrls: ['./file-home.component.css'],
})
export class FileHomeComponent implements OnInit {
    // private displayedColumns = ['fileName', 'categoryName'];
    private currentFolder: Folder = new Folder();
    private categories: Category[];
    private files: FileModel[] = [];
    private allFiles: FileModel[] = [];
    private selectedFiles: string[] = [];
    private selectionMode: boolean = false;
    private firstSelected: FileModel;

    @ViewChild(FolderHomeComponent, null)
    public folderComponent: FolderHomeComponent;

    constructor(
        private fileService: FileService,
        private categoryService: CategoryService,
        private dialog: MatDialog
    ) { }

    ngOnInit() {
        this.categoryService.getAll().subscribe(data => this.categories = data);
        this.fileService.getAll().subscribe(data => {
            this.allFiles = data;
            this.files = data.filter(file => file.folderId == null);
        });
    }

    @ViewChild(MatMenuTrigger, null)
    contextMenu: MatMenuTrigger;
    contextMenuPosition = { x: '0px', y: '0px' };

    getFolder($event) {
        this.currentFolder = $event;
        this.files = this.allFiles.filter(file => file.folderId == this.currentFolder.id);
    }

    newFile() {
        this.dialog.open(CreateFileComponent, {
            data: {
                folderId: this.currentFolder.id,
                categories: this.categories
            }
        }).afterClosed().subscribe(file => {
            if (file != null) {
                this.allFiles.push(file);
                this.files.push(file);
            }
        });
    }

    openFile(file: FileModel) {
        this.fileService.stream(file.id).subscribe(data => {
            const blob = new Blob([data], { type: 'application/octet-stream' });
            const url = window.URL.createObjectURL(blob);
            window.open(url);
        });
    }

    downloadFile(file: FileModel) {
        this.fileService.stream(file.id).subscribe(data => {
            const blob = new Blob([data], { type: 'application/octet-stream' });
            saveAs(blob, file.fileName);
        });
    }

    editFile(file: FileModel) {
        this.dialog.open(EditFileComponent, {
            data: file
        });
    }

    deleteFile(file: FileModel) {
        if (confirm(`Are you sure you want to delete '${file.fileName}'`)) {
            this.fileService.delete(file.id).subscribe(response => {
                if (response.ok) {
                    this.files = this.files.filter(f => f.id != file.id);
                    this.allFiles = this.allFiles.filter(f => f.id != file.id);
                }
            });
        }
    }

    moveFile(file: FileModel) {
        this.dialog.open(MoveFileComponent, {
            data: {
                fileToMove: file,
                allFolders: JSON.parse(JSON.stringify(this.folderComponent.detAllFolders)),
                folders: JSON.parse(JSON.stringify(this.folderComponent.getFolders)),
                path: JSON.parse(JSON.stringify(this.folderComponent.getPath)),
                currentFolder: this.currentFolder
            }
        }).afterClosed().subscribe((data) => {
            if (data != null) {
                this.files = this.files.filter(f => f.id != file.id);
                this.allFiles = this.allFiles.filter(f => f.id != file.id);
                this.allFiles.push(data);
            }
        });
    }

    selectFile(file: FileModel) {
        this.firstSelected = file;
        this.selectionMode = true;
    }

    deleteSelected() {
        this.fileService.deleteMany(this.selectedFiles).subscribe(response =>{
            if(response.ok){
                this.allFiles = this.allFiles.filter(file => !this.selectedFiles.includes(file.id));
                this.files = this.files.filter(file => !this.selectedFiles.includes(file.id));
                this.selectedFiles = null;
                this.selectionMode = false;
            }
        });
    }

    downloadSelected() {
        this.fileService.downloadMany(this.selectedFiles).subscribe(data => {
            if (data != null) {
                const blob = new Blob([data], { type: 'application/octet-stream' });
                saveAs(blob, "files.zip");
            }
        });
    }

    onContextMenu(event: MouseEvent, file: FileModel) {
        event.preventDefault();
        this.contextMenuPosition.x = event.clientX + 'px';
        this.contextMenuPosition.y = event.clientY + 'px';
        this.contextMenu.menuData = { 'file': file };
        this.contextMenu.openMenu();
    }

    selectListChange($event: FileModel) {
        if (this.selectedFiles.length == 0) {
            this.selectionMode = false;
            this.firstSelected = null;
        }
    }

    pushZip(zip, file, data) {
        const blob = new Blob([data], { type: 'application/octet-stream' });
        zip.file(file, blob);
    }
}
