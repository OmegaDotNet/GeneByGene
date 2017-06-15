import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'samplesbystatus',
    templateUrl: './samplesbystatus.component.html'
})
export class SamplesByStatusComponent {
    public samples: Sample[];
    public statuses: Status[];
    filtered: boolean;
    http: Http;

    constructor(http: Http) {
        this.http = http;
        this.filtered = false;
        this.getStatuses();
        this.getSamples();
    }

    getStatuses() {
        this.http.get('http://localhost:8380/gbg/statuses').subscribe(result => {
            this.statuses = result.json() as Status[];
        });
    }

    getSamples() {
        this.http.get('http://localhost:8380/gbg/samples').subscribe(result => {
            this.samples = result.json() as Sample[];
        });
    }

    getFilteredSamples(statusId: number) {
        if (statusId.toString() == "-1") {
            this.filtered = false;
            this.getSamples();
        }
        else {
            this.filtered = true;
            this.http.get('http://localhost:8380/gbg/samplesbystatus/' + statusId.toString()).subscribe(result => {
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

interface Status {
    StatusId: number;
    Status: string;
}