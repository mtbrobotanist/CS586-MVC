import { Component, Inject } from '@angular/core';
import { Http } from "@angular/http";
import { ApartmentComplex as AptComplex} from "./properties.component.interfaces";

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent {
    public complexes: AptComplex[];       

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'propertydata/properties').subscribe(result => {
            this.complexes = result.json() as AptComplex[];
        }, error => console.error(error));
    }

}

