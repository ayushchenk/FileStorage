import { Component } from '@angular/core';

@Component({
    selector: 'user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent {
    constructor(
    ) { }

    disable($event){
        $event.preventDefault();
    }
}       