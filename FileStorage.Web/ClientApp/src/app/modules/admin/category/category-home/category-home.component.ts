import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/service/category.service';
import { Router, RouterEvent, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { Category } from 'src/app/model/category';

@Component({
    selector: 'category-home',
    templateUrl: './category-home.component.html',
    providers: [CategoryService]
})
export class CategoryHomeComponent implements OnInit {
    private displayedColumns = ['categoryName', 'id', 'actions'];
    private categories: Category[];

    constructor(
        private categoryService: CategoryService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.router.events.pipe(
            filter((event: RouterEvent) => event instanceof NavigationEnd),
        ).subscribe(() => {
            this.categoryService.getAll().subscribe(data => this.categories = data);
        });
        this.categoryService.getAll().subscribe(data => this.categories = data);
    }

    delete(category: Category) {
        if (confirm(`Are you sure want to delete: '${category.categoryName}'`)) {
            this.categoryService.delete(category.id).subscribe(() =>
                this.router.navigate(['/admin/categories'])
            );
        }
    }
}