import { Component, OnInit } from '@angular/core';
import { FileService } from 'src/app/service/file.service';
import { FileModel } from 'src/app/model/file';
import { CategoryService } from 'src/app/service/category.service';
import { Category } from 'src/app/model/category';
import { MatSnackBar } from '@angular/material/snack-bar';
import { saveAs } from '@progress/kendo-file-saver';
import { error } from 'util';
import { UseExistingWebDriver } from 'protractor/built/driverProviders';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
    private displayedColumns = ['fileName', 'actions'];
    private categories: Category[] = [];
    private files: FileModel[] = [];
    private fileName: string;
    private selectedCategory: string = null;

    constructor(
        private fileService: FileService,
        private categoryService: CategoryService,
        private snackBar: MatSnackBar,
    ) { }

    ngOnInit() {
        this.categoryService.getAll().subscribe(data =>
            this.categories = data
        );
    }

    search() {
        this.files = [];
        if(this.fileName == ""){
            this.fileName = undefined;
        }
        this.fileService.search(this.selectedCategory, this.fileName).subscribe(data => {
            this.files = data;
            if (this.files.length == 0) {
                this.snackBar.open("No files found", "OK", { duration: 3500 });
            }
        });
    }

    download(file: FileModel) {
        this.fileService.download(file.id).subscribe(
            response => {
                if (response.ok) {
                    const blob = new Blob([response.body], { type: 'application/octet-stream' });
                    saveAs(blob, file.fileName);
                }
            }, 
            error =>
                this.snackBar.open("Error loading file", "OK", { duration: 3500 })
        );
    }
}
