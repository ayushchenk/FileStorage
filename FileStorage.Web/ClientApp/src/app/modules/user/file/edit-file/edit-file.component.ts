import { Component, OnInit, Inject } from '@angular/core';
import { FileService } from 'src/app/service/file.service';
import { MAT_DIALOG_DATA, MatDialogRef, throwMatDialogContentAlreadyAttachedError } from '@angular/material/dialog';
import { CreateFolderComponent } from '../../folder/create-folder/create-folder.component';
import { FileModel } from 'src/app/model/file';

@Component({
    selector: 'app-edit-file',
    templateUrl: './edit-file.component.html',
    styleUrls: ['./edit-file.component.css']
})
export class EditFileComponent {
    private file: FileModel;
    private originName: string;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<CreateFolderComponent>,
        private fileService: FileService,
    ) {
        this.file = data;
        this.originName = this.file.fileName;
     }

    submit() {
        this.fileService.put(this.file).subscribe();
        this.dialogRef.close()
    }

    cancel(){
        this.file.fileName = this.originName;
        this.dialogRef.close();
    }
}
