import { Component } from '@angular/core'
import { CategoryService } from 'src/app/service/category.service';
import { Guid } from 'guid-typescript';
import { Router } from '@angular/router';
import { Category } from 'src/app/model/category';

@Component({
    selector: 'create-category',
    templateUrl: './create-category.component.html',
    styleUrls: ['./create-category.component.css'],
    providers: [CategoryService],
})
export class CreateCategoryComponent {
    category: Category = new Category();

    constructor(
        private categoryService: CategoryService,
        private router: Router
    ) { }

    submit() {
        this.categoryService.post(this.category).subscribe(() =>
            this.router.navigate(['/admin/categories'])
        );
    }
}