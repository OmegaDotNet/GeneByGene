import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class SamplesService {
    constructor(private http: Http) {
        console.log('SamplesService Initialized...');
    }

    getSamples(){
        return this.http.get('http://localhost:8083/gbg/samples')
            .map(res => res.json());
    }
}