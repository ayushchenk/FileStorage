import { Component, OnInit } from '@angular/core'
import { CategoryService } from 'src/service/category.service';
import { Guid } from 'guid-typescript';
import { Category } from 'src/model/category';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'edit-category',
    templateUrl: './edit-category.component.html',
    styleUrls: ['./edit-category.component.css'],
    providers: [CategoryService],
})
export class EditCategoryComponent implements OnInit {
    category: Category = new Category(Guid.EMPTY, "");

    constructor(
        private categoryService: CategoryService,
        private router: Router,
        private route: ActivatedRoute
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.categoryService.get(params['id']).subscribe(data => this.category = data);
        });
    }

    submit() {
        this.categoryService.put(this.category).subscribe(() =>
            this.router.navigate(['/admin/categories'])
        );
    }
}