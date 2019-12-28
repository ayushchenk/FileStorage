import { Component, OnInit, Input } from '@angular/core';
import { FolderService } from 'src/app/service/folder.service';
import { Folder } from 'src/app/model/folder';

@Component({
    selector: 'app-nav-folder',
    templateUrl: './nav-folder.component.html',
    styleUrls: ['./nav-folder.component.css']
})
export class NavFolderComponent implements OnInit {
    @Input() private allFolders: Folder[];
    @Input() private folders: Folder[];
    @Input() private path: Folder[];
    @Input() private currentFolder : Folder;

    public get getCurrentFolder(): Folder {
        return this.currentFolder;
    }

    constructor(
        private folderService: FolderService
    ) { }

    ngOnInit() {
        // this.folderService.getAll().subscribe(data => {
        //     this.allFolders = data;
        //     this.folders = this.allFolders.filter(folder => folder.parentFolderId == null);
        // });
    }

    navFolder(folder: Folder) {
        this.currentFolder = folder;
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

    navRoot() {
        while (this.path.length != 0) {
            this.path.pop();
        }
        this.currentFolder = new Folder();
        this.folders = this.allFolders.filter(f => f.parentFolderId == null);
    }
}
