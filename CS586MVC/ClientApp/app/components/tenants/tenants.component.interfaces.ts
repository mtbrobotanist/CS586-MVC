export interface Tenant
{
    id:number;
    firstName:string;
    lastName:string;
    phone:string;
    email:string;
    current:boolean;
    currentLeaseId:number;
}