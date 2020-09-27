import { Component } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/throw';
import { Service } from '../service/service';


@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
})
export class UsersComponent {
    public users: Array<any> = [];
    constructor(private service: Service) {
        this.getUsers();
    }

    getUsers() {
        let self = this;
        this.service.getUsers().subscribe((res: Response) => {
                self.users = res.json();
            });
    }
    update() {
        let self = this;
        this.service.update().subscribe((res: Response) => {
            self.getUsers();
            });
    }
    delete(userId: number) {
        if (confirm("Do you want to delete the User: " + userId)) {
            let self = this;
            this.service.delete(userId).subscribe((res: Response) => {
                    self.getUsers();
                });
        }
    }
}
