import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchsamples',
    templateUrl: './fetchsamples.component.html'
})
export class FetchSamplesComponent {
    public samples: Sample[];

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get('http://localhost:8380/gbg/samples').subscribe(result => {
            this.samples = result.json() as Sample[];
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
