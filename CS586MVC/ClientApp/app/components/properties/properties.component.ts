import { Component, Inject } from '@angular/core';
import { Http } from "@angular/http";
import {ApartmentComplex, ApartmentComplex as AptComplex} from "./properties.component.interfaces";
import {VirtualTimeScheduler} from "rxjs/Rx";

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent {
    public complexes: AptComplex[];       

    constructor(private http: Http, @Inject('BASE_URL') 
    private baseUrl: string) {
        http.get(baseUrl + 'propertydata/properties').subscribe(result => {
            this.complexes = result.json() as AptComplex[];
        }, error => console.error(error));
    }
    
    public deleteComplex(complex:ApartmentComplex)
    {
        complex.deleted = true;
        let url = this.baseUrl + 'propertydata/properties/' + complex.id.toString();
        this.http.delete(url).subscribe();
    }

}

