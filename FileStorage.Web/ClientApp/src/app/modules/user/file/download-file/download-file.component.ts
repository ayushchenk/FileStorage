import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FileService } from 'src/app/service/file.service';
import { saveAs } from '@progress/kendo-file-saver';
import { FileModel } from 'src/app/model/file';

@Component({
    selector: 'app-download-file',
    templateUrl: './download-file.component.html',
    styleUrls: ['./download-file.component.css']
})
export class DownloadFileComponent implements OnInit {
    file: FileModel;
    message: string = "";

    constructor(
        private route: ActivatedRoute,
        private fileService: FileService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.fileService.download(params['id']).subscribe(response => {
                this.message = "Success";
                const blob = new Blob([response.body], { type: 'application/octet-stream' });
                saveAs(blob, response.headers.get('filename'));
            }, error => {
                this.message = "Could not download file, perhaps it's private or doesn't exists";
            });
        });
    }

}
