import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { User } from 'src/app/model/user';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
    private users: User[] = [];
    private displayedColumns = ['email', 'firstName', 'lastName', 'isAdmin', 'actions'];

    constructor(
        private userService: UserService
    ) { }

    ngOnInit() {
        this.userService.getAll().subscribe(data =>
            this.users = data
        );
    }

    makeAdmin(user: User) {
        this.userService.makeAdmin(user.email).subscribe(response => {
            if (response.ok){
                user.isAdmin = true;
            }
        });
    }

    makeUser(user: User) {
        this.userService.makeUser(user.email).subscribe(response => {
            if (response.ok){
                user.isAdmin = false;
            }
        });
    }

    delete(user: User) {
        this.userService.delete(user.id).subscribe(response => {
            if (response.ok){
                this.users = this.users.filter(u => u.id != user.id);
            }
        });
    }
}
