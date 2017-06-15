import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'samplesbyuser',
    templateUrl: './samplesbyuser.component.html'
})
export class SamplesByUserComponent {
    public samples: Sample[];
    public users: User[];
    filtered: boolean;
    criteria: boolean;
    http: Http;

    constructor(http: Http) {
        this.http = http;
        this.filtered = false;
        this.criteria = false;
        this.getUsers();
        this.getSamples();
    }

    getUsers() {
        this.http.get('http://localhost:8380/gbg/users').subscribe(result => {
            this.users = result.json() as User[];
        });
    }

    getSamples() {
        this.http.get('http://localhost:8380/gbg/samples').subscribe(result => {
            this.samples = result.json() as Sample[];
        });
    }

    getFilteredSamples(userId: number) {
        if (userId.toString() == "-1") {
            this.filtered = false;
            this.getSamples();
        }
        else {
            this.filtered = true;
            this.http.get('http://localhost:8380/gbg/samplescreatedby/' + userId.toString()).subscribe(result => {
                this.samples = result.json() as Sample[];
            });
        }
        this.criteria = false;
    }

    searchSamples(createdby: string) {
        if (createdby == "") {
            this.filtered = false;
            this.getSamples();
        }
        else {
            this.filtered = false;
            this.criteria = true;
            this.http.get('http://localhost:8380/gbg/samplescreatedby/' + createdby).subscribe(result => {
                this.samples = result.json() as Sample[];
            });
        }
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

interface User {
    UserId: number;
    FirstName: string;
    LastName: string;
}