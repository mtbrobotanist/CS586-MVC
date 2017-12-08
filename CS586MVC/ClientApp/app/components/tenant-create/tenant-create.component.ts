import {Component, OnInit, OnDestroy, Inject} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import { Http, Headers } from "@angular/http";
import { Tenant } from "../tenants/tenants.component.interfaces";

@Component({
  selector: 'app-tenant-create',
  templateUrl: './tenant-create.component.html',
  styleUrls: ['./tenant-create.component.css']
})
export class TenantCreateComponent implements OnInit, OnDestroy {

    public vmFirstName:string;
    public vmLastName:string;
    public vmPhone:string;
    public vmEmail:string;

    public tenant:Tenant;
    
    constructor(private http: Http,
                @Inject('BASE_URL') private baseUrl:
                    string, private route: ActivatedRoute) {
    }


    ngOnInit() {
        this.tenant = new Tenant();
    }
  
    ngOnDestroy() {
        
    }
    
    private copyToTenant()
    {
        this.tenant.firstName = this.vmFirstName;
        this.tenant.lastName = this.vmLastName;
        this.tenant.phone = this.vmPhone;
        this.tenant.email = this.vmEmail;
    }
    
    public submit() {
        this.copyToTenant();
        
        let url = this.baseUrl + "propertydata/tenants";

        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Date', Date.now().toString());
        
        this.http
            .post(url, JSON.stringify(this.tenant), {headers:headers})
            .subscribe(result => console.log(result),
                error => console.error(error));
    }
    
    public cancel() {
        this.vmFirstName = "";
        this.vmLastName = "";
        this.vmPhone = "";
        this.vmEmail = "";
    }

}
