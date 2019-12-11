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
    folder: Folder = new Folder(Guid.EMPTY, "", this.tokenManager.get().userId, null);

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<CreateFolderComponent>,
        private folderService: FolderService,
        private tokenManager: TokenManager,
    ) { }

    ngOnInit() {
        if (this.data != Guid.EMPTY) {
            this.folder.parentFolderId = this.data;
        }

    }

    submit() {
        this.folderService.post(this.folder).subscribe(response => {
            console.log(response);
            if (response.ok) {
                this.folder.id = response.body;
                this.dialogRef.close(this.folder);
            } else {
                this.dialogRef.close(null);
            }
        });
    }
}