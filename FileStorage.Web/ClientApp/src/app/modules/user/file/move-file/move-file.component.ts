import { Component, ViewChild, Inject } from '@angular/core';
import { NavFolderComponent } from '../../folder/nav-folder/nav-folder.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FileService } from 'src/app/service/file.service';
import { FileModel } from 'src/app/model/file';

@Component({
    selector: 'app-move-file',
    templateUrl: './move-file.component.html',
    styleUrls: ['./move-file.component.css']
})
export class MoveFileComponent {
    private fileToMove : FileModel;
    
    @ViewChild(NavFolderComponent, null)
    nav: NavFolderComponent;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<MoveFileComponent>,
        private fileService: FileService,
    ) {
        this.fileToMove = data.fileToMove;
    }

    get disabled() : boolean{
        return this.fileToMove.folderId == this.nav.getCurrentFolder.id;
    }

    cancel() {
        this.dialogRef.close(null);
    }

    move() {
        this.fileToMove.folderId = this.nav.getCurrentFolder.id;
        this.fileService.put(this.fileToMove, true).subscribe(response => {
            if(response.ok){
                this.dialogRef.close(response.body);
            }
        });
    }
}
