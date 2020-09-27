import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/Rx';
import { Service } from '../service/service';

@Component({
    selector: 'insert-user',
    templateUrl: './createUser.component.html'
})

export class CreateUserComponent implements OnInit {
    title: string = 'Create'
    errorMessage: any;
    modelData: any = {};
    myAppUrl: string = "";
    id: number = 0;

    constructor(private service: Service, private _avRoute: ActivatedRoute, public http: Http, private _router: Router, @Inject('BASE_URL') baseUrl: string) {
        debugger;
        this.myAppUrl = baseUrl;
        if (this._avRoute.snapshot.params["id"]) {
            this.id = this._avRoute.snapshot.params["id"];
        }
    }

    ngOnInit() {
        debugger;
        if (this.id > 0) {
            this.title = "Edit";
            let self = this;
            let headers = new Headers();
            headers.append('Content-Type', 'application/json; charset=utf-8');
            this.service.getoneUser(this.id).subscribe((res: Response) => {
                    self.modelData = JSON.parse(res._body);
                });
        }
    }

    save(): void {
        if (this.validateData()) {
            let self = this;
            let headers = new Headers();
            headers.append('Content-Type', 'application/json; charset=utf-8');
            if (this.title == "Create") {
                this.service.insert(this.modelData).subscribe((res: Response) => {
                    self._router.navigate(['/users']);
                    });
            }
            if (this.title == "Edit") {
                this.service.update(this.id, this.modelData).subscribe((res: Response) => {
                    self._router.navigate(['/users']);
                    });
            }
        }
    }

    private validateData(): boolean {
        let status: boolean = true;
        let strMessage: string = '';
        if (this.isNullOrUndefined(this.modelData)) {
            status = false;
            strMessage = 'Fill the the Fields in the Forms';
        }
        else if (this.isNullOrUndefined(this.modelData.name)) {
            status = false;
            strMessage = 'Please enter the name...';
        }
        else if (this.isNullOrUndefined(this.modelData.email)) {
            status = false;
            strMessage = 'Please enter the email...';
        }
        else if (this.isNullOrUndefined(this.modelData.status)) {
            status = false;
            strMessage = 'Please enter the status...';
        }
        if (status === false)
            alert(strMessage);
        return status;
    }

    isNullOrUndefined(data: any): boolean {
        return this.isUndefined(data) || this.isNull(data);
    }

    isUndefined(data: any): boolean {
        return typeof (data) === "undefined";
    }

    isNull(data: any): boolean {
        return data === null;
    }

    cancel() {
        this._router.navigate(['/users']);
    }

} 
