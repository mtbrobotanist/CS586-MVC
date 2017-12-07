import { Tenant } from "../tenants/tenants.component.interfaces";
import {ApartmentComplex} from "../properties/properties.component.interfaces";

export class Lease {
    id:number;
    tenantId:number;
    apartmentComplexUnitId:number;
    startDate:number;
    durationMonths:number;
    rentMonthly:number;
    active:boolean;
    apartmentComplexUnit:ApartmentComplexUnit;
    tenant:Tenant;
    deleted:boolean = false;
}

export class ApartmentComplexUnit
{
    id:number;
    bedRooms:number;
    bathRooms:number;
    area:number;
    apartmentComplexId:number;
    unitNumber:number;
    occupied:boolean;
    address:string;
    apartmentComplex:ApartmentComplex;
}