import { Tenant } from "../tenants/tenants.component.interfaces";
import {ApartmentComplex} from "../properties/properties.component.interfaces";

export interface Lease {
    id:number;
    personId:number;
    apartmentComplexUnitId:number;
    startDate:string;
    durationMonths:string;
    rentMonthly:number;
    active:boolean;
    endDate:string;
    apartmentComplexUnit:ApartmentComplexUnit;
    tenant:Tenant;
}

export interface ApartmentComplexUnit
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