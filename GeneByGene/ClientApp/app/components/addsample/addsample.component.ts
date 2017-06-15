import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'addsample',
    templateUrl: './addsample.component.html'
})
export class AddSampleComponent {    
    public users: User[];    
    public statuses: Status[];
    public user: number;
    public status: number;
    public sample: Sample;
    public today: number = Date.now();
    http: Http;

    constructor(http: Http) {
        this.http = http;        
        this.getUsers();
        this.getStatuses();
    }

    getUsers() {
        this.http.get('http://localhost:8380/gbg/users').subscribe(result => {
            this.users = result.json() as User[];
        });
    }

    getStatuses() {
        this.http.get('http://localhost:8380/gbg/statuses').subscribe(result => {
            this.statuses = result.json() as Status[];
        });
    }

    setUser(userId: number) {
        if (userId != null) {
            this.user = userId;
        }
    }

    setStatus(statusId: number) {
        if (statusId != null) {
            this.status = statusId;
        }
    }

    addSample(barcode: string, date: string) {
        this.http.get('http://localhost:8380/gbg/samples/addsample?barcode=' + barcode + '&createdAt=' + date + '&createdBy=' + this.user.toString() + '&statusId=' + this.status.toString()).subscribe(result => {
            this.statuses = result.json() as Status[];
        });
    }
}

interface Sample {
    SampleId: number;
    Barcode: string;
    CreatedAt: string;
    CreatedById: number;
    CreatedBy: string;
    StatusId: number;
    Status: string;
}

interface Status {
    StatusId: number;
    Status: string;
}

interface User {
    UserId: number;
    FirstName: string;
    LastName: string;
}