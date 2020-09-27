import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class Service {

    usersAppUrl: string = "";
    headers: Headers = new Headers();

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.usersAppUrl = baseUrl;
        this.headers.append('Content-Type', 'application/json; charset=utf-8');
    }
    getUsers() {
        return this.http.get(this.usersAppUrl + '/api/Users', { headers: this.headers })
    }
    getoneUser(id: number) {
        return this.http.get(this.usersAppUrl + "api/Users/" + id, { headers: this.headers })

    }
    delete(userId: number) {
        if (confirm("Do you want to delete the User: " + userId)) {
          return  this.http.delete(this.usersAppUrl + "api/Users/" + userId, { headers: this.headers })
        }
    }
    insert(modelData: any) {
       return this.http.post(this.usersAppUrl + "api/Users", modelData, { headers: this.headers })
    }
    update(userId: number, modelData: any) {
       return this.http.put(this.usersAppUrl + "api/Users/" + userId, modelData, { headers: this.headers })
    }
}
