import { Tenant } from "../tenants/tenants.component.interfaces";
import {AptComplex} from "../properties/properties.component.interfaces";

export interface Lease {
    id:number;
    startDate:string;
    durationMonths:number;
    endDate:string;
    rentMonthly:number;
    active:boolean;
    tenant:Tenant;
    unit:AptComplexUnit;
}

export interface AptUnit
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