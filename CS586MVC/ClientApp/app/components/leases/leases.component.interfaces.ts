import { Tenant } from "../tenants/tenants.component.interfaces";
import {ApartmentComplex} from "../properties/properties.component.interfaces";

export interface Lease {
    id:number;
    tenantId:number;
    apartmentComplexUnitId:number;
    startDate:number;
    durationMonths:string;
    rentMonthly:number;
    active:boolean;
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