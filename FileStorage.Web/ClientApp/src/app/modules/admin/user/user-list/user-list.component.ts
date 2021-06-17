import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { User } from 'src/app/model/user';
import { MatTableDataSource } from '@angular/material/table';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
    private dataSource = new MatTableDataSource<User>();
    private displayedColumns = ['email', 'firstName', 'lastName', 'isAdmin', 'actions'];

    constructor(
        private userService: UserService
    ) {
        this.dataSource.filterPredicate = this.filterPredicate;
     }

    ngOnInit() {
        this.userService.getAll().subscribe(data =>
            this.dataSource.data = data
        );
    }

    makeAdmin(user: User) {
        this.userService.makeAdmin(user.email).subscribe(response => {
            if (response.ok) {
                user.isAdmin = true;
            }
        });
    }

    makeUser(user: User) {
        this.userService.makeUser(user.email).subscribe(response => {
            if (response.ok) {
                user.isAdmin = false;
            }
        });
    }

    delete(user: User) {
        this.userService.delete(user.id).subscribe(response => {
            if (response.ok) {
                this.dataSource.data = this.dataSource.data.filter(u => u.id != user.id);
            }
        });
    }

    applyFilter(filterValue: string) {
        this.dataSource.filter = filterValue.trim().toLowerCase();
    }

    filterPredicate(data : User, filter: string){
        if(data.firstName == null){
            data.firstName = "";
        }
        if(data.lastName == null){
            data.lastName = "";
        }
        filter = filter.trim().toLowerCase();
        return data.email.toLowerCase().indexOf(filter) !== -1 
            || data.firstName.toLowerCase().indexOf(filter) !== -1 
            || data.lastName.toLowerCase().indexOf(filter) !== -1 
            || (data.isAdmin && filter == "admin")
            || (!data.isAdmin && filter == "user")
    }
}
