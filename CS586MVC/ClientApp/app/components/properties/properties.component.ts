import { Component, Inject } from '@angular/core';
import {Http } from "@angular/http";

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent {
    public properties: Property[];       

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'PropertyData/properties').subscribe(result => {
            this.properties = result.json() as Property[];
        }, error => console.error(error));
    }

}

interface Property {
id:number;
address:string;
}

