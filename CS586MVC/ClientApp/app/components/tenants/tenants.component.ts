import { Component, Inject } from '@angular/core';
import { Http } from "@angular/http";

@Component({
  selector: 'app-tenants',
  templateUrl: './tenants.component.html',
  styleUrls: ['./tenants.component.css']
})
export class TenantsComponent {

    public tenants:Tenant[];
    
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'propertydata/tenants').subscribe(result => {
            this.tenants = result.json() as Tenant[];
        }, error => console.error(error));
    }

}

interface Tenant
{
    id:number;
    firstName:string;
    lastName:string;
    phone:string;
    email:string;
    current:boolean;
    address:string;
}
