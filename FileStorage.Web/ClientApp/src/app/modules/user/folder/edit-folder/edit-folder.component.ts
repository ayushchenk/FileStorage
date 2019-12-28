import { Component, OnInit, Inject } from '@angular/core'
import { FolderService } from 'src/app/service/folder.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateFolderComponent } from '../create-folder/create-folder.component';
import { Folder } from 'src/app/model/folder';

@Component({
    selector: 'edit-folder',
    templateUrl: './edit-folder.component.html',
    styleUrls: ['./edit-folder.component.css'],
})
export class EditFolderComponent implements OnInit {
    private folder: Folder;
    private originName: string;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<EditFolderComponent>,
        private folderService: FolderService,
    ) {
        this.folder = this.data;
    }

    ngOnInit() {
        this.originName = this.folder.folderName;
    }

    submit() {
        this.folderService.put(this.folder, false).subscribe();
        this.dialogRef.close()
    }

    cancel() {
        this.folder.folderName = this.originName;
        this.dialogRef.close();
    }
}