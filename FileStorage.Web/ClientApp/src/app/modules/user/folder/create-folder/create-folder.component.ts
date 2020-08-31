import { Component, OnInit, Inject } from "@angular/core";
import { FolderService } from 'src/app/service/folder.service';
import { Guid } from 'guid-typescript';
import { TokenManager } from 'src/app/modules/account/service/token-manager';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Folder } from 'src/app/model/folder';

@Component({
    selector: 'create-folder',
    templateUrl: './create-folder.component.html',
    styleUrls: ['./create-folder.component.css'],
})
export class CreateFolderComponent implements OnInit {
    private folder: Folder = new Folder();

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<CreateFolderComponent>,
        private folderService: FolderService,
        private tokenManager: TokenManager,
    ) { }

    get nameFree(): boolean {
        return !this.data.folders.map(f => f.folderName).includes(this.folder.folderName);
    }

    ngOnInit() {
        this.folder.userId = this.tokenManager.get().userId;
        if (this.data.id != Guid.EMPTY) {
            this.folder.parentFolderId = this.data.id;
        }
    }

    submit() {
        this.folderService.post(this.folder).subscribe(response => {
            if (response.ok) {
                this.folder.id = response.body;
                this.dialogRef.close(this.folder);
            } else {
                this.dialogRef.close(null);
            }
        });
    }
}