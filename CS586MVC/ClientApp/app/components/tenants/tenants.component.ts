import { Component, Inject } from '@angular/core';
import { Http } from "@angular/http";
import { Tenant } from "./tenants.component.interfaces";

@Component({
  selector: 'app-tenants',
  templateUrl: './tenants.component.html',
  styleUrls: ['./tenants.component.css']
})
export class TenantsComponent {

    public tenants:Tenant[];
    
    constructor(private http: Http, 
                @Inject('BASE_URL') private baseUrl: string) {
        http.get(baseUrl + 'propertydata/tenants').subscribe(result => {
            this.tenants = result.json() as Tenant[];
        }, error => console.error(error));
    }
    
    public deleteTenant(tenant:Tenant)
    {
        tenant.current = false;
        
        let url = this.baseUrl + 'propertydata/tenants/' + tenant.id.toString();
        this.http.delete(url).subscribe();
    }

}
