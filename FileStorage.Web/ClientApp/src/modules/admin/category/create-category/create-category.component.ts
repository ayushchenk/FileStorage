import { Component } from '@angular/core'
import { CategoryService } from 'src/service/category.service';
import { Guid } from 'guid-typescript';
import { Category } from 'src/model/category';
import { Router } from '@angular/router';

@Component({
    selector: 'create-category',
    templateUrl: './create-category.component.html',
    styleUrls: ['./create-category.component.css'],
    providers: [CategoryService],
})
export class CreateCategoryComponent {
    category: Category = new Category(Guid.EMPTY, "");

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