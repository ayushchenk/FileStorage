import { Component, OnInit, Inject } from '@angular/core';
import { CategoryService } from 'src/app/service/category.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FileService } from 'src/app/service/file.service';
import { TokenManager } from 'src/app/modules/account/service/token-manager';
import { Guid } from 'guid-typescript';
import { FileModel, FileAccessibility } from 'src/app/model/file';
import { Category } from 'src/app/model/category';

@Component({
    selector: 'app-create-file',
    templateUrl: './create-file.component.html',
    styleUrls: ['./create-file.component.css']
})
export class CreateFileComponent implements OnInit {
    private fileDTO: FileModel = new FileModel();
    private file: File;
    private categories: Category[];

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<CreateFileComponent>,
        private fileService: FileService,
        private tokenManager: TokenManager
    ) { }

    ngOnInit() {
        this.categories = this.data.categories;
        this.fileDTO.userId = this.tokenManager.get().userId;
        this.fileDTO.folderId = this.data.folderId;
        if (this.fileDTO.folderId == null) {
            this.fileDTO.folderId = Guid.EMPTY;
        }
    }

    submit() {
        this.fileDTO.fileName = this.file.name;
        this.fileDTO.path = this.file.name;
        this.fileDTO.file = this.file;
        this.fileService.post(this.fileDTO).subscribe(response => {
            if (response.ok) {
                this.fileDTO.id = response.body;
                this.dialogRef.close(this.fileDTO);
            } else {
                this.dialogRef.close(null);
            }
        });
    }

    handleFileInput(files: FileList) {
        this.file = files.item(0);
    }
}
