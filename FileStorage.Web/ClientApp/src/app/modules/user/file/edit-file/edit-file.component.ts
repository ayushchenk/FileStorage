import { Component, OnInit, Inject } from '@angular/core';
import { FileService } from 'src/app/service/file.service';
import { MAT_DIALOG_DATA, MatDialogRef, throwMatDialogContentAlreadyAttachedError } from '@angular/material/dialog';
import { CreateFolderComponent } from '../../folder/create-folder/create-folder.component';
import { FileModel, FileAccessibility } from 'src/app/model/file';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { UrlService } from 'src/app/service/url.service';

@Component({
    selector: 'app-edit-file',
    templateUrl: './edit-file.component.html',
    styleUrls: ['./edit-file.component.css'],
})
export class EditFileComponent implements OnInit {
    private file: FileModel;
    private originName: string;
    private accessibility = [
        { value: FileAccessibility.Private, display: 'Private', description: 'Only seen to you' },
        { value: FileAccessibility.Protected, display: 'Protected', description: 'Access via link' },
        { value: FileAccessibility.Public, display: 'Public', description: 'Open for everyone' },
    ];

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<CreateFolderComponent>,
        private fileService: FileService,
        private urlService: UrlService
    ) {
        this.file = data;
        this.originName = this.file.fileName;
    }

    ngOnInit() {
        if (this.file.fileAccessibility != FileAccessibility.Protected) {
            this.urlService.getUrl(this.file.id).subscribe(response => {
                this.file.shortLink = response;
            });
        }
    }

    submit() {
        if (this.file.fileAccessibility != FileAccessibility.Protected) {
            this.file.shortLink = null;
        } 
        this.fileService.put(this.file).subscribe();
        this.dialogRef.close()
    }

    cancel() {
        this.file.fileName = this.originName;
        this.dialogRef.close();
    }
}
