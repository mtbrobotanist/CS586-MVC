import { Component, Inject } from '@angular/core';
import { Http } from "@angular/http";
import { Tenant } from "../tenants/tenants.component";
import { AptComplex } from "../properties/properties.component";

@Component({
  selector: 'app-leases',
  templateUrl: './leases.component.html',
  styleUrls: ['./leases.component.css']
})
export class LeasesComponent {

    public leases:Lease[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'propertydata/leases').subscribe(result => {
            this.leases = result.json() as Lease[];
        }, error => console.error(error));
    }
  
}

interface Lease {
    id:number;
    startDate:string;
    durationMonths:number;
    rentMonthly:number;
    active:boolean;
    tenant:Tenant;
    unit:AptComplexUnit;
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
    aptComplex:AptComplex;
}