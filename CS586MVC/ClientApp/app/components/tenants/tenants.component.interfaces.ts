export class Tenant
{
    id:number;
    firstName:string;
    lastName:string;
    phone:string;
    email:string;
    current:boolean;
    currentLeaseId:number;
}

function tenantCopy (src: Tenant, dest: Tenant)
{
    let tenantSrc = src as Tenant;
    let tenantDest = dest as Tenant;
    
    tenantDest.id = tenantSrc.id;
    tenantDest.firstName = tenantSrc.firstName.toString();
    tenantDest.lastName = tenantSrc.lastName.toString();
    tenantDest.phone = tenantSrc.phone.toString();
    tenantDest.email = tenantSrc.email.toString();
    tenantDest.current = tenantSrc.current;
    tenantDest.currentLeaseId = tenantSrc.currentLeaseId;
}