import { Component, Inject } from '@angular/core';
import {Http } from "@angular/http";

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent {
    public complexes: AptComplex[];       

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'PropertyData/properties').subscribe(result => {
            this.complexes = result.json() as AptComplex[];
        }, error => console.error(error));
    }

}

interface AptUnit
{
    id:number;
    bedRooms:number;
    bathRooms:number;
    area:number;
}

export interface AptComplexUnit
{
    id:number;
    aptUnitId:number;
    aptComplexId:number;
    unitNumber:number;
    address:string;
    
    aptUnit:AptUnit;
}

interface AptComplex
{
    id:number;
    address:string;
    size:number;
    occupiedCount:number;
}

