import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { FetchSamplesComponent } from './components/fetchsamples/fetchsamples.component';
import { SamplesByStatusComponent } from './components/fetchsamples/samplesbystatus.component';
import { SamplesByUserComponent } from './components/fetchsamples/samplesbyuser.component';
import { CounterComponent } from './components/counter/counter.component';
import { AddSampleComponent } from './components/addsample/addsample.component';


export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        FetchSamplesComponent,
        SamplesByStatusComponent,
        SamplesByUserComponent,
        AddSampleComponent,
        HomeComponent        
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'fetch-samples', component: FetchSamplesComponent },
            { path: 'samples-by-status', component: SamplesByStatusComponent },
            { path: 'samples-by-user', component: SamplesByUserComponent },
            { path: 'add-sample', component: AddSampleComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
};
